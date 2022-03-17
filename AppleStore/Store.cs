using System;
using System.Collections.Generic;
using System.Text;

namespace AppleStore

{
    public class Store
    {
        public int id { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public List<Product> Product { get; set; }
        public int quantity { get; set; }
    }
}
