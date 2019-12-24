using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels
{
    public class EditRoleViewModel
    {
        
        public string Id { get; set; }
        [Required(ErrorMessage ="Role name is required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
    }
}
