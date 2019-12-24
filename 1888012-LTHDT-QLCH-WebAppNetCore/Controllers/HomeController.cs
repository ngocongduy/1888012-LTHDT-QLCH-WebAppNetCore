using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _1888012_LTHDT_QLCH_WebAppNetCore.Models;
using Microsoft.AspNetCore.Hosting;
using _1888012_LTHDT_QLCH_WebAppNetCore.DAL;
using System.IO;
using _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;      

        /*
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
        _logger = logger;
        }
        */

        //DI constructor: when HomeController is called, it will request a DI service
        //DI service will create a MockProductRepository and inject to this constructor
        //WebHostEnvironment is default service
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;        
        }

        public IActionResult Index()
        {
            return View();
        }

        //Method to make a Dictionary combining Products and ProductTypes
        private Dictionary<string, List<Product>> typeProductPairs()
        {
            Dictionary<string, List<Product>> typeProductPairs = new Dictionary<string, List<Product>>();
            foreach (ProductType type in productRepository.GetProductTypeList())
            {
                typeProductPairs.Add(type.Name, new List<Product>());
                foreach (Product product in productRepository.GetProductList())
                {
                    if (product.Type == type.Name)
                    {
                        typeProductPairs[type.Name].Add(product);
                    }
                }
            }
            return typeProductPairs;
        }

        [HttpPost]
        public IActionResult StockByType()
        {
            if (ModelState.IsValid)
            {
                SummaryViewModel model = new SummaryViewModel();
                var typeProductPairs = this.typeProductPairs();
                foreach (var type in typeProductPairs.Keys)
                {
                    typeProductPairs[type].RemoveAll(p => p.Stock <= 0);
                }
                model.typeProductPairs = typeProductPairs;
                return View(model);

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult OutOfDate()
        {
            if (ModelState.IsValid)
            {
                SummaryViewModel model = new SummaryViewModel();
                var typeProductPairs = this.typeProductPairs();
                foreach (var type in typeProductPairs.Keys)
                {
                    typeProductPairs[type].RemoveAll(p => p.ExpiredDate > DateTime.Now);
                }
                model.typeProductPairs = typeProductPairs;
                return View(model);
            }
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
