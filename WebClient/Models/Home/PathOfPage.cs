using System.Collections.Generic;

namespace WebClient.Models.Home
{
    public class POrder
    {
        public string OrderName { get; set; }
        public string OrderValue { get; set; }
    }
    public class PathOfPage
    {
        public PathOfPage()
        {
            IsProduct = 1;
            Page = 1;
            PageSize = 0;
            TotalPage = 0;
            Total = 0;
            PathName = new List<string>();
            pOrders = new List<POrder>();

            pOrders.Add(new POrder() { OrderName = "Thứ tự theo mức độ phổ biến", OrderValue = "popularity" });
            pOrders.Add(new POrder() { OrderName = "Thứ tự theo điểm đánh giá", OrderValue = "rating" });
            pOrders.Add(new POrder() { OrderName = "Mới nhất", OrderValue = "date" });
            pOrders.Add(new POrder() { OrderName = "Thứ tự theo giá: thấp đến cao", OrderValue = "price" });
            pOrders.Add(new POrder() { OrderName = "Thứ tự theo giá: cao xuống thấp", OrderValue = "price-desc" });
        }
        public List<string> PathName { get; set; }

        public int IsProduct { get; set; } = 1;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int Total { get; set; }

        public List<POrder> pOrders { get; set; }
    }
}
