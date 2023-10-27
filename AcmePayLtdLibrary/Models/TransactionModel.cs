using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public Decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardholderNumber { get; set; }
        public string HolderName { get; set;}
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set;}
        public int CVV { get; set; }
        public string? AuthorizeOrderReference { get; set; }
        public string? VoidOrderReference { get; set; }
        public string? CaptureOrderReference { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
