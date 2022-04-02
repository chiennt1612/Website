using System;
using System.Collections.Generic;

namespace WebClient.Models.Order
{
    public class CreateOrder
    {
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public double Total { get; set; }
    }

    public class OrderItem
    {
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public int Units { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
    }
}
