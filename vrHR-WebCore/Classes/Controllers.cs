using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vrHR_WebCore.Classes
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class PaymentModel
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public int CVV { get; set; }
        public double Amount { get; set; }
        public string Expiry { get; set; }
    }
}
