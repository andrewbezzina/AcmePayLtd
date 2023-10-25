using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Models.Request
{
    public class AuthorizeTransactionModel
    {
        public Decimal Amount { get; set; }
        public string Currency { get; set; } // TODO validate that is valid currency code
        public string CardholderNumber { get; set; }
        public string HolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVV { get; set; }
        public string OrderReference { get; set; }
    }
}
