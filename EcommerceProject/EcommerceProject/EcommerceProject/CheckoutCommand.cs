using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceProject
{
    internal class CheckoutCommand
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
