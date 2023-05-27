using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseProject.Models.EF
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        
    }
}