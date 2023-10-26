using System.Text.Json.Serialization;

namespace AcmePayLtdLibrary.Models.Response
{
    public class StatusResponseModel
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}