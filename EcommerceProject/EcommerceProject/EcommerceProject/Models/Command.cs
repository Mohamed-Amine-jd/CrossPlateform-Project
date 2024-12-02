using System;
using System.Collections.Generic;

namespace EcommerceProject
{
    public class Command
    {
        public string id { get; set; }
        public string userId { get; set; }
      
        public List<Product> products { get; set; }
        public double amount { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

       

        public class Product
        {
            public string id { get; set; }
            public int quantity { get; set; }

            public string productName { get; set; }




            // 
        }
    }
}
