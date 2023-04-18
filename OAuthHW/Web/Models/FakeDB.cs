using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthHW.Web.Models
{
    public class FakeDB
    {
        public UserInfo[] users { get; set; }
    }

    public class UserInfo
    {
        public string name { get; set; } = "";
        public string sub { get; set; } = "";
        public string access_token { get; set; } = "";
    }
}

