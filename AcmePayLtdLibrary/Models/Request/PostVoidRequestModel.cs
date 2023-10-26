namespace AcmePayLtdLibrary.Models.Request
{
    public class PostVoidRequestModel
    {
        public Guid Id { get; set; }
        public string OrderReference { get; set; }
    }
}