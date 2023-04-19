using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using OAuthHW.Common;
using OAuthHW.Web.Models;

namespace OAuthHW.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LineController : ControllerBase
    {
        public async Task<IActionResult> Login(string code, string state)
        {
            //TODO: Check State是否一致
            //取得code , state
            CommFuns.WriteLog("[LineLogin] 收到Login Call Back - code、state");
            #region Line OpenID Login & Verify
            //向認證server換access token / open id
            CommFuns.WriteLog("[LineLogin] Get Token by Code Post：https://api.line.me/oauth2/v2.1/token");
            var valuePairs = new Dictionary<string, string>();
            valuePairs.Add("grant_type", "authorization_code");
            valuePairs.Add("code", code);
            valuePairs.Add("redirect_uri", CommFuns.LineLogin_RedirectUrl);
            valuePairs.Add("client_id", CommFuns.LineLogin_ClientID);
            valuePairs.Add("client_secret", CommFuns.LineLogin_Client_secret);
            string sResult = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync("https://api.line.me/oauth2/v2.1/token", new FormUrlEncodedContent(valuePairs));
                sResult = response.Content.ReadAsStringAsync().Result;
            }

            //verify id token
            CommFuns.WriteLog("[LineLogin] Verify Id Token Post：https://api.line.me/oauth2/v2.1/verify");
            LineToken token = JsonConvert.DeserializeObject<LineToken>(sResult);
            var valuePairs2 = new Dictionary<string, string>();
            valuePairs2.Add("client_id", CommFuns.LineLogin_ClientID);
            valuePairs2.Add("id_token", token.id_token);
            sResult = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync("https://api.line.me/oauth2/v2.1/verify", new FormUrlEncodedContent(valuePairs2));
                sResult = response.Content.ReadAsStringAsync().Result;
            }
            LineProfile profile = JsonConvert.DeserializeObject<LineProfile>(sResult);
            #endregion

            CommFuns.WriteLog("[LineLogin] Done");

            //TODO : if fail ,than ....
            CommFuns.Main_ReportLineLoginResult(profile);

            //return 200
            return Ok();
        }

        public async Task<IActionResult> Notify(string code, string state)
        {
            CommFuns.WriteLog("[LineNotify] 收到Notify Call Back - code、state");
            //如果db上這個user 沒有notfiy的access token的話，就要提示user要去同意加入
            //如果已有的話，去檢查access token有效性，如果無效的話，提示user重新同意加入或是refresh
            //向認證server換access token / open id
            CommFuns.WriteLog("[LineNotify] Get Token by Code Post：https://notify-bot.line.me/oauth/token");
            var valuePairs = new Dictionary<string, string>();
            valuePairs.Add("grant_type", "authorization_code");
            valuePairs.Add("code", code);
            valuePairs.Add("redirect_uri", CommFuns.LineNotify_RedirectUrl); //the same with line login callbackurl
            valuePairs.Add("client_id", CommFuns.LineNotify_ClientID);
            valuePairs.Add("client_secret", CommFuns.LineNotify_Client_secret);
            string sResult = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync("https://notify-bot.line.me/oauth/token", new FormUrlEncodedContent(valuePairs));
                sResult = response.Content.ReadAsStringAsync().Result;
            }

            LineNotifyToken token = JsonConvert.DeserializeObject<LineNotifyToken>(sResult);

            CommFuns.WriteLog("[LineNotify] Get Token by Code Done");

            CommFuns.Main_ReportLineNotifyResult(token);

            return Ok();
        }

    }
}
