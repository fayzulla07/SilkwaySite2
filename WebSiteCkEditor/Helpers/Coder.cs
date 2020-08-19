using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebSiteCkEditor.Helpers
{
    public static class Coder
    {
        public static string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public static string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }
    }
}