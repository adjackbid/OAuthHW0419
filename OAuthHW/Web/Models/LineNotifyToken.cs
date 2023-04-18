using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthHW.Web.Models
{
    public class LineNotifyToken
    {
        public string access_token { get; set; }

        public int status { get; set;}

        public string message { get; set; }


    }
}
