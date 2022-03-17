using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppleStore
{
    public class Product
    {
        public int id { get; set; }
        public string itemName { get; set; }
        public int itemCost { get; set; }
        public string version { get; set; }
        public int diagonal { get; set; }
        
        public List<Store> Store { get; set; }
        [ForeignKey("Technologyid")]
        public virtual Technology Technology { get; set; }
    }
}
