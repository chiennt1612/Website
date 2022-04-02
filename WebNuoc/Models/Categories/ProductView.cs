using System;

namespace WebNuoc.Models.Categories
{
    public class ProductView
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quota { get; set; }
        public long TimeAdd { get; set; }
        public ProductView()
        {
            TimeAdd = DateTime.Now.Ticks;
        }
    }
}
