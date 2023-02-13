namespace WebAdmin.Helpers
{
    public class WebConfig
    {
        public string CookieName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ResponseType { get; set; }
        public System.Collections.Generic.IList<string> Scope { get; set; }
        public string WebName { get; set; } = "BacNgocTuan";
    }
}
