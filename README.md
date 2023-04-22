# OAuthHW0419
目標：整合line login、登入後提供line notify連動申請功能 <br />
開發平台：<br />
.NET 6 <br />
Winform APP<br />

## Line Login概念：<br />
1. Windows打開程式執行檔(免安裝) - OAuthHW.exe <br />
2. APP中提供Login功能 → 點選後開啟系統預設broswer並帶入line取code(授權碼)網址 <br />
註：此時同時APP會自動host一個web service - web api (.NET Core Kestrel) <br />
<pre>
<code>
string sUrl = CommFuns.baseUrl;
WebHoster hoster = new WebHoster(sUrl);
host = hoster.CreateWebHostBuilder().Build();
host.RunAsync();
</code>
</pre>
<pre>https://access.line.me/oauth2/v2.1/authorize?response_type=code&client_id={LineLogin_ClientID}&state={數亂取得state}&scope=openid profile&redirect_uri={LineLogin_RedirectUrl}</pre>
3. 系統預設broswer上進行line登入確認動作後，會呼叫打回callback redirect url <br />
4. callback url網址被呼後，會回到APP的web api - LineController中，此時取得到code、state <br />
5. 驗證state是否與step2中一致<br />
6. 拿code去取得access token<br />
<pre>https://api.line.me/oauth2/v2.1/token</pre>
7. 拿到access token後，驗證token是否有效 <br />
<pre>Post：https://api.line.me/oauth2/v2.1/verify</pre>
註：此step可以同時拿到使用者的sub, 將sub存在DB中以作為使用者與LINE之間的身份ID<br />

## Line Notify連動概念：<br />
1. 檢查DB中，使用者是否已有LineNotify Access Token<br />
2. 若有Notify Access Token的話，驗證此Access Token是否有效<br />
<pre>https://notify-api.line.me/api/status</pre>
3. 若無Notify Access Token的話，提供使用者申請Line Notify連動功能，點選後開始系統預設broswer並帶入notify連動網址<br />
<pre>https://notify-bot.line.me/oauth/authorize?response_type=code&client_id={LineNotify_ClientID}&redirect_uri={LineNotify_RedirectUrl}&scope=notify&state={數亂取得state}</pre>
4. 使用者在預設browser上點選確認後，會導回callback網址，此時會回到APP Host的Web API (Line/Notify)<br />
5. Line/Notify API得到code, State後，同樣進行驗證state並用code取得Access Token<br />
<pre>https://notify-bot.line.me/oauth/token</pre>
6. 回傳成功後，儲存此Access Token至DB中(for之後要發送給這個使用者Notify Message時用)<br />

## Line Notify Send Message概念：<br />
1. 使用者在畫面中選擇要發送的對象<br />
2. 使用者在畫面中輸入要發送的訊息<br />
3. 按下發送後<br />
4. APP從DB抓取使用者Notify Access Token<br />
5. APP使用此Access Token執行Notify API<br />
<pre>https://notify-api.line.me/api/notify</pre>

## Line Notify取消連動概念：<br />
1. 使用者在APP中點選取消連動
2. APP呼叫Line Revoke API並傳入此Access Token
<pre>https://notify-api.line.me/api/revoke</pre>
