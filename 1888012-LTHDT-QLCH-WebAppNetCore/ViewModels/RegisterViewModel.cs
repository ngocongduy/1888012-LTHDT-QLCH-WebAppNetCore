using _1888012_LTHDT_QLCH_WebAppNetCore.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailValid", controller: "Account")] //For remote validation
        [ValidEmailDomain(allowedDomain: "gmail.com",
            ErrorMessage = "Valid domain is gmail.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
