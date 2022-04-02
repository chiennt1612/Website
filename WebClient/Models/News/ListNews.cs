namespace WebClient.Models.News
{
    public class ListNews
    {
        public string Keyword { get; set; }
        public long CategoryId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
