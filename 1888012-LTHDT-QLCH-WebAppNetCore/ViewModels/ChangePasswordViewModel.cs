﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword",ErrorMessage ="The new password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
