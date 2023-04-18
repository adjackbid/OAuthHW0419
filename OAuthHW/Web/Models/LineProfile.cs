using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthHW.Web.Models
{
    public class LineProfile
    {
        public string iss { get; set; }
        public string sub { get; set; }
        public string name { get; set; }
        public string picture { get; set; }

        public string error { get; set; } = "";

        public string error_description { get; set; } = "";
    }
}
