using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MyStore.Models
{
    [Serializable]
    public class Product
    {
        public Product()
        {

        }

        public int ProductId { get; set; }
        [Display(Name = "Products name")]
        [Required(ErrorMessage = "Input products name")]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Input products price")]
        public int Price { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Input desciption")]
        public string Description { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.Now;
        [XmlIgnore]
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}