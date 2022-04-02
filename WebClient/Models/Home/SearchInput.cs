namespace WebClient.Models.Home
{
    public class SearchInput
    {
        public int? Page { get; set; }
        public int? TotalRow { get; set; }
        public int? TotalPage { get; set; }
        public long? CategoryId { get; set; }
        public string Keyword { get; set; }
        public string SearchType { get; set; }
        public double? min_price { get; set; }
        public double? max_price { get; set; }
        public bool IsDiscount { get; set; }

        public string OrderValue { get; set; }
        public SearchInput()
        {
            OrderValue = "date";
            IsDiscount = false;
            Keyword = "";
            CategoryId = 0;
            Page = 1;
            TotalRow = 0;
            TotalPage = 0;
            min_price = 0;
            max_price = 99999999;
            SearchType = "Product";
        }
    }
}
