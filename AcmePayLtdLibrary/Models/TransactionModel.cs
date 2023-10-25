using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Models
{
    public enum Status
    {
        Authorized = 1,
        Captured = 2,
        Voided = 3
    }
    public class TransactionModel
    {
        public int Id { get; set; }
        public Decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardholderNumber { get; set; }
        public string HolderName { get; set;}
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set;}
        public int CVV { get; set; }
        public string OrderReference { get; set; }
        public string UUID { get; set; } 
        public Status Status { get; set; }
    }
}
