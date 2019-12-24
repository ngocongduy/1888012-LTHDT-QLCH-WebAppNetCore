using _1888012_LTHDT_QLCH_WebAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels
{
    public class StockOutViewModel : StockOutDetail
    {
        public List<StockOutDetail> outList { get; set; }
    }
}
