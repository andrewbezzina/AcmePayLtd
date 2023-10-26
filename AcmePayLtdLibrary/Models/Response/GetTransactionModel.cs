using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AcmePayLtdLibrary.Models.Response
{
    public class GetTransactionModel
    {
        public Decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardholderNumber { get; set; }
        public string HolderName { get; set; }
        public string Id { get; set; } //Uuid
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}
