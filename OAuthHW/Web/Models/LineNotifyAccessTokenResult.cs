using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthHW.Web.Models
{
    public class LineNotifyAccessTokenResult
    {
        public string message { get; set; }
        public int status { get; set; }

        public string targetType { get; set; }

        public string target { get; set; }
    }
}
