using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class Product
    {
       
        public int ProductID { get; set; }
        [Required(ErrorMessage = "please enter a product name")]
        public string Name { get; set;}
        [Required(ErrorMessage = "please enter a Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "please enter a positive Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "please specify a category")]
        public string Category { get; set; }
       
    }
}
