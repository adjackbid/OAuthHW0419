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

        //�n�J
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //�´��աAWindows APP�۬[web api
                string sUrl = CommFuns.baseUrl;
                WebHoster hoster = new WebHoster(sUrl);
                host = hoster.CreateWebHostBuilder().Build();
                host.RunAsync();
                CommFuns.WriteLog($"[LineLogin] Service Up {sUrl}");
                
                //�}��Browser
                //�üƨ��ostate
                CommFuns.Line_State = Guid.NewGuid().ToString();
                Process.Start(new ProcessStartInfo(CommFuns.LineLogin_GetCodeUrl) { UseShellExecute = true });
                CommFuns.WriteLog($"[LineLogin] Get Code Start");
                //���X�@�ӵ����צ�
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
            lblName.Text = "Name�G" + profile?.name;
            lblNotifyStatus.Text = "LineNotify���A�G" + "�ݽT�{";
            CommFuns.WriteLog($"[LineLogin] �{�ҵ��G:{((profile.error=="")?"OK":profile.error)}");
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

        //�n��J
        private async void btnCheckNotify_Click(object sender, EventArgs e)
        {
            try
            {
                CommFuns.WriteLog($"[LineNotify] �ˬd�ϥΪ�Notify Access Token�O�_�s�b");
                var userInfo = CommFuns.GetUserInfoFromDB(_profile.sub);
                if (userInfo != null && userInfo.access_token != "")
                {
                    CommFuns.WriteLog($"[LineNotify] �w�s�b�A�ˬdAccess Token�O�_������");
                    var result = await CommFuns.LineAccessTokenCheck(userInfo.access_token);
                    CommFuns.WriteLog($"[LineNotify] �{�ҵ��G:{result.status}");
                    if (result.status == 200)
                    {
                        lblNotifyStatus.Text = "LineNotify���A�G" + "OK";
                        btnNotifyApply.Enabled = false;
                        btnRevokeNotify.Enabled = true;
                    }
                    else
                    {
                        CommFuns.WriteLog($"[LineNotify] Access Token�L�ġA�ШϥΪ̶i��Nofity�s��");
                        btnNotifyApply.Enabled = true;
                        btnRevokeNotify.Enabled = false;
                    }
                }
                else
                {
                    CommFuns.WriteLog($"[LineNotify] �{�ҵ��G:�L��ơA�ШϥΪ̶i��Nofity�s��");
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
            CommFuns.WriteLog($"[LineNotify] �{�ҵ��G:{token.status} {token.message}");
            if (token.status == 200)
            {
                lblNotifyStatus.Text = "LineNotify���A�G" + "OK";
                CommFuns.WriteLog($"[LineNotify] �s�ʦ��\ Token:{token.access_token.Substring(0,5)}...");
                btnRevokeNotify.Enabled = true;
            }
            else
            {
                lblNotifyStatus.Text = "LineNotify���A�G" + "�ݳs��";
                CommFuns.WriteLog($"[LineNotify] �s�ʥ���:{token?.status} {token?.message}");
                btnRevokeNotify.Enabled = false;
            }

            //��s��Ʈw(access token)
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
                CommFuns.WriteLog($"[LineNotify] ���oLineNotify�{��Code");
                //�}��Browser
                //�üƨ��ostate
                CommFuns.Line_State = Guid.NewGuid().ToString();
                Process.Start(new ProcessStartInfo(CommFuns.LineNotify_GetCodeUrl) { UseShellExecute = true });
                CommFuns.WriteLog($"[LineNotify] Get Code Start");
                //�������צ�
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
                    throw new Exception("�ϥΪ̨S���j�wNotify�s�ʬ���");
                }

                CommFuns.WriteLog($"[LineNotify] Revoke Start");
                var result = await CommFuns.LineNotifyRevoke(userInfo.access_token);
                CommFuns.WriteLog($"[LineNotify] Revoke���G:{result.status}");
                if (result.status == 200)
                {
                    lblNotifyStatus.Text = "LineNotify���A�G" + "�w����";
                    btnNotifyApply.Enabled = true;
                    btnRevokeNotify.Enabled = false;
                    //��s�ϥΪ�access_token
                    userInfo.access_token = "";//�M��
                    CommFuns.UpdateOrInsertUserInfo(userInfo);
                }
                else
                {
                    CommFuns.WriteLog($"[LineNotify] Revoke���ѡG{result.message}");
                    btnNotifyApply.Enabled = false;
                    btnRevokeNotify.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //��x
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
                    throw new Exception("����ܵo�e��H!!");
                }

                if(rtbMessage.Text == "")
                {
                    throw new Exception("����J�o�e�T��!!");
                }
                var selectItem = (ComboboxItem)ddlTarget.SelectedItem;
                //Check Access token is valid
                CommFuns.WriteLog($"[LineNotify] �o�e�T��");
                var result = await CommFuns.LineNotifySendMessage(selectItem.Value.ToString(),
                    rtbMessage.Text.Trim());
                CommFuns.WriteLog($"[LineNotify] �o�e���G:{result.status}");
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
