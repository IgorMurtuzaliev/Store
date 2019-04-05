using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Input you name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Input you surname")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Input you email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Register date")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfRegister { get; set; } = DateTime.Now;

        public virtual ICollection<Order> Orders { get; set; }
    }
}