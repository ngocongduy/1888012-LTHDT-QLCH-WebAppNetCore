using _1888012_LTHDT_QLCH_WebAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels
{
    public class ProductTypeViewModel
    {
        public string PageTitle = "Add a Product Type";
        public List<ProductType> productTypes;
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        public DateTime DateAdded { get; set; }
        public string Status { get; set; }

    }
}
