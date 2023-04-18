using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OAuthHW.Web
{
    public class WebHoster
    {
        private string _webUrl = "";

        public WebHoster(string webUrl)
        {
            _webUrl = webUrl;
        }

        public IWebHostBuilder CreateWebHostBuilder() =>
           WebHost.CreateDefaultBuilder()
               .UseStartup<Startup>()
               .UseUrls(_webUrl);
    }
}
