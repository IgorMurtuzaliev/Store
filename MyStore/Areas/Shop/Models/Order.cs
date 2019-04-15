using MyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Areas.Shop.Models
{
    public class Order
    {
        public Order()
        {
            ProductOrders = new List<ProductOrder>();
        }

        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of order")]
        public DateTime TimeOfOrder { get; set; } = DateTime.Now;
        public string UserId { get; set; }

        public virtual User User { get; set; }
        public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}