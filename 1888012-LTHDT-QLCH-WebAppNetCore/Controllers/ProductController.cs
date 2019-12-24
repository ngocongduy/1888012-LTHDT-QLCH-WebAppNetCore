using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _1888012_LTHDT_QLCH_WebAppNetCore.DAL;
using _1888012_LTHDT_QLCH_WebAppNetCore.Models;
using _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;        

        //DI constructor: when HomeController is called, it will request a DI service
        //DI service will create a MockProductRepository and inject to this constructor
        //WebHostEnvironment is a default service 
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        //Actions for Product
        public IActionResult Detail()
        {
            ProductViewModel model = new ProductViewModel();
            model.products = productRepository.GetProductList();
            model.DateAdded = DateTime.Now;
            model.MfgDate = DateTime.Now;
            model.ExpiredDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateAProduct(ProductViewModel model)
        {
            List<Product> products = productRepository.GetProductList();
            if (ModelState.IsValid)
            {
                Product newProduct = new Product
                {
                    Id = products.Last().Id + 1,
                    Name = model.Name,
                    Type = model.Type,
                    DateAdded = model.DateAdded,
                    MfgName = model.MfgName,
                    MfgDate = model.MfgDate,
                    ExpiredDate = model.ExpiredDate,
                    Stock = model.Stock
                };
                productRepository.AddAProduct(newProduct);


            }
            return RedirectToAction("Detail", "Product");
        }
        [HttpPost]
        public IActionResult UpdateAProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                //model.products = productRepository.GetProductList();
                Product newProduct = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Type = model.Type,
                    DateAdded = model.DateAdded,
                    MfgName = model.MfgName,
                    MfgDate = model.MfgDate,
                    ExpiredDate = model.ExpiredDate,
                    Stock = model.Stock
                };
                productRepository.UpdateAProduct(model.Id, newProduct);


            }
            return RedirectToAction("Detail", "Product");
        }
        [HttpPost]
        public IActionResult DeleteAProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    productRepository.DeleteAProduct(model.Id);
                }                
            }
            return RedirectToAction("Detail", "Product");
        }

        [HttpGet]
        public IActionResult Search()
        {
            ProductViewModel model = new ProductViewModel();
            model.products = productRepository.GetProductList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Search(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.products = productRepository.SearchProducts(model.Id, model.Name, model.ExpiredDate, model.Stock);
                return View(model);
            }
            return RedirectToAction("Detail", "Product");
        }

        //Actions for ProductType
        public IActionResult ProductType()
        {
            ProductTypeViewModel model = new ProductTypeViewModel();
            model.productTypes = productRepository.GetProductTypeList();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateAProductType(ProductTypeViewModel model)
        {
            List<ProductType> productTypes = productRepository.GetProductTypeList();
            if (ModelState.IsValid)
            {
                ProductType newProductType = new ProductType
                {
                    Id = productTypes.Last().Id + 1,
                    Name = model.Name,
                    DateAdded = model.DateAdded,
                    Status = model.Status
                };
                productRepository.AddAProductType(newProductType);


            }
            return RedirectToAction("ProductType", "Product");
        }
        [HttpPost]
        public IActionResult UpdateAProductType(ProductTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductType newProductType = new ProductType
                {
                    Id = model.Id,
                    Name = model.Name,
                    DateAdded = model.DateAdded,
                    Status = model.Status
                };
                productRepository.UpdateAProductType(model.Id, newProductType);

            }
            return RedirectToAction("ProductType", "Product");
        }
        [HttpPost]
        public IActionResult DeleteAProductType(ProductTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    productRepository.DeleteAProductType(model.Id);
                }
            }
            return RedirectToAction("ProductType", "Product");
        }

        [HttpGet]
        public IActionResult SearchType()
        {
            ProductTypeViewModel model = new ProductTypeViewModel();
            model.productTypes = productRepository.GetProductTypeList();
            return View(model);
        }
        [HttpPost]
        public IActionResult SearchType(ProductTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.productTypes = productRepository.SearchProductTypes(model.Id, model.Name, model.Status);
                return View(model);
            }
            return RedirectToAction("ProductType", "Product");
        }
    }
}