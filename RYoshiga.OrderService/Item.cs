using System;

namespace RYoshiga.OrderService
{
    public class Item
    {
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public Guid ProductId { get; set; }
    }
}