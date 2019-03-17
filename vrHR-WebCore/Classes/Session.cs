using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vrHR_WebCore.Classes
{
    public static class Session
    {
        public static string connection = "Data Source=CRUHWK-PC\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;" + 
            "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static bool isLoggedIn {get; set; }
        public static string LoggedInUserName { get; set; }
        public static string SessionCode { get; set; }
        public static double Amount { get; set; }
        public static bool Paying { get; set; }
        public static bool IsHR { get; set; }

    }
}
