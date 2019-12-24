using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Models
{
    public interface IProductRepository
    {
        //Product
        List<Product> GetProductList();
        Product GetAProduct(int id);
        bool AddAProduct(Product newProduct);
        bool UpdateAProduct(int id, Product newProduct);
        bool DeleteAProduct(int id);
        List<Product> SearchProducts(int id, string name, DateTime expiredDate, int stock);

        //Product type
        List<ProductType> GetProductTypeList();
        ProductType GetAProductType(int id);
        bool AddAProductType(ProductType newProductType);
        bool UpdateAProductType(int id, ProductType newProductType);
        bool DeleteAProductType(int id);
        List<ProductType> SearchProductTypes(int id, string name, string status);

    }
}
