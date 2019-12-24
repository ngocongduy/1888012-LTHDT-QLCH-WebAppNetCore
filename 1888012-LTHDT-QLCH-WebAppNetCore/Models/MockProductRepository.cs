using _1888012_LTHDT_QLCH_WebAppNetCore.DAL;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Models
{
    public class MockProductRepository : IProductRepository
    {
        private readonly IWebHostEnvironment webHostingEnvironment;
        private const string folder = "data";
        private const string productFile = "product.json";
        private const string typeFile = "product_type.json";

        private List<Product> products { get; set; }
        private List<ProductType> productTypes { get; set; }
        public MockProductRepository(IWebHostEnvironment webHostingEnvironment)
        {
            this.webHostingEnvironment = webHostingEnvironment;
            string rootPath = Path.Combine(this.webHostingEnvironment.WebRootPath, folder);
            products = LocalDataAccess.ReadDataProduct(rootPath, productFile);
            productTypes = LocalDataAccess.ReadDataProductType(rootPath, typeFile);
        }

        //Product methods
        public List<Product> GetProductList()
        {
            return products;
        }

        public Product GetAProduct(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public bool AddAProduct(Product newProduct)
        {
            int index = products.FindIndex(p => p.Id == newProduct.Id);
            if (index >= 0)
            {
                return false;
            }
            products.Add(newProduct);
            string rootPath = Path.Combine(webHostingEnvironment.WebRootPath, folder);
            LocalDataAccess.WriteDataProduct(rootPath, productFile, products);
            return true;
        }

        public bool UpdateAProduct(int id, Product newProduct)
        {
            int index = products.FindIndex(p => p.Id == id);
            if (index >= 0)
            {
                products[index].Name = newProduct.Name;
                products[index].DateAdded = newProduct.DateAdded;
                products[index].MfgName = newProduct.MfgName;
                products[index].MfgDate = newProduct.MfgDate;
                products[index].ExpiredDate = newProduct.DateAdded;
                products[index].Type = newProduct.Type;
                products[index].Stock = newProduct.Stock;
                string rootPath = Path.Combine(webHostingEnvironment.WebRootPath, folder);
                LocalDataAccess.WriteDataProduct(rootPath, productFile, products);
                return true;
            }
            return false;
        }

        public bool DeleteAProduct(int id)
        {
            int index = products.FindIndex(p => p.Id == id);
            if (index >= 0)
            {
                products.RemoveAt(index);
                string rootPath = Path.Combine(webHostingEnvironment.WebRootPath, folder);
                LocalDataAccess.WriteDataProduct(rootPath, productFile, products);
                return true;
            }
            return false;
        }
        // Explicit predicate delegate.
        private static bool FindProductName(Product p, string name)
        {
            bool isNameMatched = true;
            if (name != null)
            {
                isNameMatched = p.Name.Contains(name,StringComparison.OrdinalIgnoreCase);
            }
            return isNameMatched;
        }
        public List<Product> SearchProducts(int id, string name, DateTime expiredDate, int stock)
        {
            List<Product> searchProducts = new List<Product>();
            if (id > 0)
            {
                return products.FindAll(p => p.Id == id);
            }
            else
            {
                searchProducts = products.FindAll(p => p.ExpiredDate >= expiredDate || p.Stock >= stock);
                if (searchProducts.Count > 0)
                {
                    return searchProducts.FindAll(p => FindProductName(p, name));
                }
                return products.FindAll(p => FindProductName(p, name));

            }
        }


        //ProductType methods
        public List<ProductType> GetProductTypeList()
        {
            return productTypes;
        }

        public ProductType GetAProductType(int id)
        {
            return productTypes.FirstOrDefault(t => t.Id == id);
        }

        public bool AddAProductType(ProductType newProductType)
        {
            int index = productTypes.FindIndex(p => p.Id == newProductType.Id);
            if (index >= 0)
            {
                return false;
            }
            productTypes.Add(newProductType);
            string rootPath = Path.Combine(webHostingEnvironment.WebRootPath, folder);
            LocalDataAccess.WriteDataProductType(rootPath, typeFile, productTypes);
            return true;
        }

        public bool UpdateAProductType(int id, ProductType newProductType)
        {
            int index = productTypes.FindIndex(p => p.Id == id);
            if (index >= 0)
            {
                productTypes[index].Name = newProductType.Name;
                productTypes[index].DateAdded = newProductType.DateAdded;
                productTypes[index].Status = newProductType.Status;

                string rootPath = Path.Combine(webHostingEnvironment.WebRootPath, folder);
                LocalDataAccess.WriteDataProductType(rootPath, typeFile, productTypes);
                return true;
            }
            return false;
        }

        public bool DeleteAProductType(int id)
        {
            int index = productTypes.FindIndex(p => p.Id == id);
            if (index >= 0)
            {
                productTypes.RemoveAt(index);
                string rootPath = Path.Combine(webHostingEnvironment.WebRootPath, folder);
                LocalDataAccess.WriteDataProductType(rootPath, typeFile, productTypes);
                return true;
            }
            return false;
        }

        private static bool FindTypeName(ProductType t, string name)
        {
            if (name != null)
            {
                return t.Name.Contains(name, StringComparison.OrdinalIgnoreCase);
            }
            return true;
        }

        public List<ProductType> SearchProductTypes(int id, string name, string status)
        {
            List<ProductType> searchProductTypes = new List<ProductType>();
            if (id > 0)
            {
                return productTypes.FindAll(p => p.Id == id);
            }
            else
            {
                searchProductTypes = productTypes.FindAll(t => t.Status == status);
                if (searchProductTypes.Count > 0)
                {
                    return searchProductTypes.FindAll(t => FindTypeName(t, name));
                }
                return productTypes.FindAll(t => FindTypeName(t, name));

            }
        }
    }
}
