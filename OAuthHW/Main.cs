using Microsoft.AspNetCore.Hosting;
using System.Windows.Forms;
using OAuthHW.Web;
using static System.Windows.Forms.DataFormats;
using OAuthHW.Common;
using System.Diagnostics;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using System;
using OAuthHW.Web.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace OAuthHW
{
    public partial class Main : Form
    {

        public PopDialog popDialog { get; set; }
        private IWebHost host { get; set; }
        private LineProfile _profile { get; set; }

        public Main()
        {
            InitializeComponent();
        }

        //登入
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //純測試，Windows APP自架web api
                string sUrl = CommFuns.baseUrl;
                WebHoster hoster = new WebHoster(sUrl);
                host = hoster.CreateWebHostBuilder().Build();
                host.RunAsync();
                CommFuns.WriteLog($"[LineLogin] Service Up {sUrl}");
                
                //開啟Browser
                //亂數取得state
                CommFuns.Line_State = Guid.NewGuid().ToString();
                Process.Start(new ProcessStartInfo(CommFuns.LineLogin_GetCodeUrl) { UseShellExecute = true });
                CommFuns.WriteLog($"[LineLogin] Get Code Start");
                //跳出一個視窗擋住
                popDialog = new PopDialog(host);
                popDialog.StartPosition = FormStartPosition.CenterParent;
                popDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ShowLineLoginResult(LineProfile profile)
        {
            //host.StopAsync();
            //host.Dispose();
            _profile = profile;
            lblName.Text = "Name：" + profile?.name;
            lblNotifyStatus.Text = "LineNotify狀態：" + "待確認";
            CommFuns.WriteLog($"[LineLogin] 認證結果:{((profile.error=="")?"OK":profile.error)}");
            if(profile!= null)
            {
                var user = CommFuns.GetUserInfoFromDB(profile.sub);
                if(user == null)
                {
                    UserInfo new_user = new UserInfo()
                    {
                        sub = _profile.sub,
                        name = _profile.name
                    };
                    CommFuns.UpdateOrInsertUserInfo(new_user);
                }
            }
        }

        //登後入
        private async void btnCheckNotify_Click(object sender, EventArgs e)
        {
            try
            {
                CommFuns.WriteLog($"[LineNotify] 檢查使用者Notify Access Token是否存在");
                var userInfo = CommFuns.GetUserInfoFromDB(_profile.sub);
                if (userInfo != null && userInfo.access_token != "")
                {
                    CommFuns.WriteLog($"[LineNotify] 已存在，檢查Access Token是否仍有效");
                    var result = await CommFuns.LineAccessTokenCheck(userInfo.access_token);
                    CommFuns.WriteLog($"[LineNotify] 認證結果:{result.status}");
                    if (result.status == 200)
                    {
                        lblNotifyStatus.Text = "LineNotify狀態：" + "OK";
                        btnNotifyApply.Enabled = false;
                        btnRevokeNotify.Enabled = true;
                    }
                    else
                    {
                        CommFuns.WriteLog($"[LineNotify] Access Token無效，請使用者進行Nofity連動");
                        btnNotifyApply.Enabled = true;
                        btnRevokeNotify.Enabled = false;
                    }
                }
                else
                {
                    CommFuns.WriteLog($"[LineNotify] 認證結果:無資料，請使用者進行Nofity連動");
                    btnNotifyApply.Enabled = true;
                    btnRevokeNotify.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ShowLineNotifyResult(LineNotifyToken token)
        {
            CommFuns.WriteLog($"[LineNotify] 認證結果:{token.status} {token.message}");
            if (token.status == 200)
            {
                lblNotifyStatus.Text = "LineNotify狀態：" + "OK";
                CommFuns.WriteLog($"[LineNotify] 連動成功 Token:{token.access_token.Substring(0,5)}...");
                btnRevokeNotify.Enabled = true;
            }
            else
            {
                lblNotifyStatus.Text = "LineNotify狀態：" + "待連動";
                CommFuns.WriteLog($"[LineNotify] 連動失敗:{token?.status} {token?.message}");
                btnRevokeNotify.Enabled = false;
            }

            //更新資料庫(access token)
            UserInfo user = new UserInfo()
            {
                access_token = token.access_token,
                sub = _profile.sub,
                name = _profile.name
            };
            CommFuns.UpdateOrInsertUserInfo(user);

        }

        private void btnNotifyApply_Click(object sender, EventArgs e)
        {
            try
            {
                CommFuns.WriteLog($"[LineNotify] 取得LineNotify認證Code");
                //開啟Browser
                //亂數取得state
                CommFuns.Line_State = Guid.NewGuid().ToString();
                Process.Start(new ProcessStartInfo(CommFuns.LineNotify_GetCodeUrl) { UseShellExecute = true });
                CommFuns.WriteLog($"[LineNotify] Get Code Start");
                //跳視窗擋住
                if (popDialog != null) { popDialog.Close(); }
                popDialog = new PopDialog(host);
                popDialog.StartPosition = FormStartPosition.CenterParent;
                popDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void btnRevokeNotify_Click(object sender, EventArgs e)
        {
            try
            {
                var userInfo = CommFuns.GetUserInfoFromDB(_profile.sub);
                if (userInfo == null || userInfo.access_token == "")
                {
                    throw new Exception("使用者沒有綁定Notify連動紀錄");
                }

                CommFuns.WriteLog($"[LineNotify] Revoke Start");
                var result = await CommFuns.LineNotifyRevoke(userInfo.access_token);
                CommFuns.WriteLog($"[LineNotify] Revoke結果:{result.status}");
                if (result.status == 200)
                {
                    lblNotifyStatus.Text = "LineNotify狀態：" + "已取消";
                    btnNotifyApply.Enabled = true;
                    btnRevokeNotify.Enabled = false;
                    //更新使用者access_token
                    userInfo.access_token = "";//清空
                    CommFuns.UpdateOrInsertUserInfo(userInfo);
                }
                else
                {
                    CommFuns.WriteLog($"[LineNotify] Revoke失敗：{result.message}");
                    btnNotifyApply.Enabled = false;
                    btnRevokeNotify.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //後台
        private void btnGetTargetList_Click(object sender, EventArgs e)
        {
            try
            {
                var fakedb = CommFuns.GetUsersFromDB();
                ddlTarget.Items.Clear();
                foreach (var user in fakedb.users)
                {
                    if(user.access_token != "")
                    {
                        ddlTarget.Items.Add(new ComboboxItem() {Text = user.name, Value = user.access_token });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private async void btnSendNotify_Click(object sender, EventArgs e)
        {
            try
            {
                if(ddlTarget.Text == "")
                {
                    throw new Exception("未選擇發送對象!!");
                }

                if(rtbMessage.Text == "")
                {
                    throw new Exception("未輸入發送訊息!!");
                }
                var selectItem = (ComboboxItem)ddlTarget.SelectedItem;
                //Check Access token is valid
                CommFuns.WriteLog($"[LineNotify] 發送訊息");
                var result = await CommFuns.LineNotifySendMessage(selectItem.Value.ToString(),
                    rtbMessage.Text.Trim());
                CommFuns.WriteLog($"[LineNotify] 發送結果:{result.status}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Make log go to buttom
        private void rlogBox_TextChanged(object sender, EventArgs e)
        {
            rlogBox.SelectionStart= rlogBox.Text.Length;
            rlogBox.ScrollToCaret();
        }
    }
}
