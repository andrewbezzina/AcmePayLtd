namespace AcmePayLtdLibrary.Models.Request
{
    public class PostCaptureRequestModel
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}