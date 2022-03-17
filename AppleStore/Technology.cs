using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppleStore
{
    public class Technology
    {
        [Key]
        public int id { get; set; }
        public string processorName { get; set; }
        public int yearIssue { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
