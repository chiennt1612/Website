using System.Collections.Generic;

namespace WebNuoc.Models.Home
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
            TotalPage = 0;
            Total = 0;
            PathName = new List<string>();
            pOrders = new List<POrder>();
        }
        public List<string> PathName { get; set; }

        public int IsProduct { get; set; } = 1;
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int Total { get; set; }

        public List<POrder> pOrders { get; set; }
    }
}
