using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.ViewModels
{
    public class OrderViewModel
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public DateTime TimeOfOrder { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
    }
}