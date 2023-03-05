namespace WebNuoc.Models
{
    public class ResponseOK
    {
        public dynamic data { get; set; }
        public int? Status { get; set; }
        public int? Code { get; set; }
        public string? UserMessage { get; set; }
        public string? InternalMessage { get; set; }
        public string? MoreInfo { get; set; }
    }
}
