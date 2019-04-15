using MyStore.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Areas.Shop.Models
{
    public class ProductOrder
    {
        public int Id { get; set; }

        [Display(Name = "Amount")]
        public int Amount { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}