using _1888012_LTHDT_QLCH_WebAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels
{
    public class StockInViewModel : StockInDetail
    {
        /*
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime DateAdded { get; set; }
        public Dictionary<int, int> StockChange { get; set; }
        public List<Product> ProductList { get; set; }
        */
        public List<StockInDetail> inList { get; set; }
    }
}
