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


    }
}
