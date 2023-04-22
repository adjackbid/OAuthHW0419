using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OAuthHW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAuthHW.Common
{
    public static partial class CommFuns
    {
        #region 應該在某個api裡的東西
        public static string baseUrl = "http://localhost:6789";
        public static string LineLogin_RedirectUrl = $"{baseUrl}/Line/Login";
        public static string LineLogin_ClientID = "1660895388";
        public static string LineLogin_GetCodeUrl = $"https://access.line.me/oauth2/v2.1/authorize?response_type=code&client_id={LineLogin_ClientID}&state=123123&scope=openid profile&redirect_uri={LineLogin_RedirectUrl}";
        public static string LineLogin_Client_secret = "0271df881c4a3c78b1502431b921213d";

        public static string LineNotify_RedirectUrl = $"{baseUrl}/Line/Notify";
        public static string LineNotify_ClientID = "aNMfm0WvCiG7VWWleFhgje";
        public static string LineNotify_Client_secret = "o9L2R7PrQFq9uSJJIfBzwUIgdhVrN4MMUwGZHqz1rRr";
        public static string LineNotify_GetCodeUrl = $"https://notify-bot.line.me/oauth/authorize?response_type=code&client_id={LineNotify_ClientID}&redirect_uri={LineNotify_RedirectUrl}&scope=notify&state=12345";
        #endregion

        /// <summary>
        /// 顯示在畫面方的LOG
        /// </summary>
        /// <param name="sMessage"></param>
        /// <param name="sLogType"></param>
        public static void WriteLog(string sMessage, string sLogType = "")
        {
            Program.MainForm.Invoke(new Action(() =>
            {
                Program.MainForm.rlogBox.AppendText($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {sLogType} {sMessage}" + "\r\n");
            }));
        }

        /// <summary>
        /// 更新主頁面：Line Login結果回報
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="profile"></param>
        public static void Main_ReportLineLoginResult(LineProfile profile)
        {
            try
            {
                Program.MainForm.Invoke(new Action(() =>
                {
                    Program.MainForm.popDialog?.Close();
                    Program.MainForm.ShowLineLoginResult(profile);
                    if (profile.error == "")
                    {
                        Program.MainForm.btnCheckNotify.Enabled = true;
                    }
                    formGoOnTop();
                }));
            }
            catch (Exception ex)
            {
                WriteLog("error=" + ex.Message);
            }
        }

        /// <summary>
        /// 更新主頁面：Line Notify註冊結果回報
        /// </summary>
        /// <param name="token"></param>
        public static void Main_ReportLineNotifyResult(LineNotifyToken token)
        {
            try
            {
                Program.MainForm.Invoke(new Action(() =>
                {
                    Program.MainForm.popDialog?.Close();
                    Program.MainForm.ShowLineNotifyResult(token);
                    formGoOnTop();
                }));
            }
            catch (Exception ex)
            {
                WriteLog("error=" + ex.Message);
            }
        }

        private static void formGoOnTop()
        {
            Program.MainForm.BringToFront();
            Program.MainForm.Activate();
        }
    }
}
