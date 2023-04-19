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
    //這裡是Line Token檢查、發送Notify相關功能(HTTP)
    public static partial class CommFuns
    {
        /// <summary>
        /// 確認Access token是否有效(line notify)
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static async Task<LineNotifyAccessTokenResult> LineAccessTokenCheck(string access_token)
        {
            string sResult = "";
            CommFuns.WriteLog("[LineNotify] Check Token Status Get：https://notify-api.line.me/api/status");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                var response = await client.GetAsync("https://notify-api.line.me/api/status");
                sResult = response.Content.ReadAsStringAsync().Result;
            }
            return JsonConvert.DeserializeObject<LineNotifyAccessTokenResult>(sResult);
        }

        /// <summary>
        /// 發送LineNotify訊息
        /// </summary>
        /// <param name="target_access_token"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<LineNotifyResult> LineNotifySendMessage(string target_access_token,string message)
        {
            string sResult = "";
            CommFuns.WriteLog("[LineNotify] Send Message Post：https://notify-api.line.me/api/notify");
            using (HttpClient client = new HttpClient())
            {
                var valuePairs = new Dictionary<string, string>();
                valuePairs.Add("message", message);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", target_access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync("https://notify-api.line.me/api/notify", new FormUrlEncodedContent(valuePairs));
                sResult = response.Content.ReadAsStringAsync().Result;
            }
            return JsonConvert.DeserializeObject<LineNotifyResult>(sResult);
        }

        /// <summary>
        /// 取消LineNotify連動 
        /// </summary>
        /// <param name="target_access_token"></param>
        /// <returns>
        /// status = 200 he request is accepted, revoking all access tokens and ending the process
        /// status = 401 the access tokens have already been revoked 
        /// </returns>
        public static async Task<LineNotifyResult> LineNotifyRevoke(string target_access_token)
        {
            string sResult = "";
            CommFuns.WriteLog("[LineNotify] Revoke Post：https://notify-api.line.me/api/revoke");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", target_access_token);
                var response = await client.PostAsync("https://notify-api.line.me/api/revoke",null);
                sResult = response.Content.ReadAsStringAsync().Result;
            }
            return JsonConvert.DeserializeObject<LineNotifyResult>(sResult);
        }
    }
}
