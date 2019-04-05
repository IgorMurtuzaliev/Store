using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    [Serializable]
    public class Account
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Input your name")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Input your password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm your password")]
        public string ConfirmedPasword { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Input you email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}