using System;
using System.Collections.Generic;

namespace RYoshiga.HotChocolateDemo.Models
{
    public class Customer
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; } =
            new List<Order>();
    }
}