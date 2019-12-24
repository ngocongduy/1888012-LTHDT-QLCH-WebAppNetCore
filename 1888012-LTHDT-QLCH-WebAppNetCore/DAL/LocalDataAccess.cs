using _1888012_LTHDT_QLCH_WebAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.DAL
{
    public class LocalDataAccess
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true
        };
        //Write & Read functions for product
        public static bool WriteDataProduct(string rootPath, string fileName, List<Product> productList)
        {
            string filePath = Path.Combine(rootPath, fileName);
            string text = JsonSerializer.Serialize(productList, options);
            /*
            using (var stream = new StreamWriter(filePath))
            {
                stream.WriteLine(text);
            }
            */
            try
            {
                File.WriteAllText(filePath, text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static List<Product> ReadDataProduct(string rootPath, string fileName)
        {             
            string filePath = Path.Combine(rootPath, fileName);
            string text = "";
            /*
            using (var stream = new StreamReader(filePath))
            {
               text = stream.ReadToEnd();
            }
            */

            if (File.Exists(filePath))
            {
                text = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Product>>(text, options);
            }
            return null;             
        }

        //Write & Read functions for product type
        public static bool WriteDataProductType(string rootPath, string fileName, List<ProductType> productTypeList)
        {
            string filePath = Path.Combine(rootPath, fileName);
            string text = JsonSerializer.Serialize(productTypeList, options);
            try
            {
                File.WriteAllText(filePath, text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static List<ProductType> ReadDataProductType(string rootPath, string fileName)
        {
            string filePath = Path.Combine(rootPath, fileName);
            string text = "";

            if (File.Exists(filePath))
            {
                text = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<ProductType>>(text, options);
            }
            return null;
        }
        
        //Write & Read functions for stock detail tracking
        //This class is used to store all IEnumerable properties as string
        private class SupportClass
        {
            public int detailId { get; set; }
            public string detailUser { get; set; }
            public DateTime detailDateAdded { get; set; }
            public string detailProductList { get; set; }
            public string detailStockChange { get; set; }
        }
        public static bool WriteDataStockIn(string rootPath, string fileName, List<StockInDetail> trackList)
        {
            List<SupportClass> writeList = new List<SupportClass>();

            string filePath = Path.Combine(rootPath, fileName);
            foreach (var item in trackList)
            {
                string products = JsonSerializer.Serialize(item.detailProductList);
                string changes = JsonSerializer.Serialize(item.detailStockChange);
                SupportClass itemNew = new SupportClass
                {
                    detailId = item.detailId,
                    detailDateAdded = item.detailDateAdded,
                    detailUser = item.detailUser,
                    detailProductList = products,
                    detailStockChange = changes
                };
                writeList.Add(itemNew);
            }

            string text = JsonSerializer.Serialize(writeList,options);

            try
            {
                File.WriteAllText(filePath, text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static List<StockInDetail> ReadDataStockIn(string rootPath, string fileName)
        {
            string filePath = Path.Combine(rootPath, fileName);
            string text = "";

            if (File.Exists(filePath))
            {
                text = File.ReadAllText(filePath);
                try
                {
                    var readList =  JsonSerializer.Deserialize<List<SupportClass>>(text, options);
                    List<StockInDetail> trackList = new List<StockInDetail>();
                    foreach (var item in readList)
                    {
                        List<Product> products = JsonSerializer.Deserialize<List<Product>>(item.detailProductList);
                        List <StockTracker> changes = JsonSerializer.Deserialize<List<StockTracker>>(item.detailStockChange);
                        StockInDetail itemNew = new StockInDetail
                        {
                            detailId = item.detailId,
                            detailDateAdded = item.detailDateAdded,
                            detailUser = item.detailUser,
                            detailProductList = products,
                            detailStockChange = changes
                        };
                        trackList.Add(itemNew);
                    }

                    return trackList; 
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
        public static bool WriteDataStockOut(string rootPath, string fileName, List<StockOutDetail> trackList)
        {
            List<SupportClass> writeList = new List<SupportClass>();

            string filePath = Path.Combine(rootPath, fileName);
            foreach (var item in trackList)
            {
                string products = JsonSerializer.Serialize(item.detailProductList);
                string changes = JsonSerializer.Serialize(item.detailStockChange);
                SupportClass itemNew = new SupportClass
                {
                    detailId = item.detailId,
                    detailDateAdded = item.detailDateAdded,
                    detailUser = item.detailUser,
                    detailProductList = products,
                    detailStockChange = changes
                };
                writeList.Add(itemNew);
            }

            string text = JsonSerializer.Serialize(writeList, options);

            try
            {
                File.WriteAllText(filePath, text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static List<StockOutDetail> ReadDataStockOut(string rootPath, string fileName)
        {
            string filePath = Path.Combine(rootPath, fileName);
            string text = "";

            if (File.Exists(filePath))
            {
                text = File.ReadAllText(filePath);
                try
                {
                    var readList = JsonSerializer.Deserialize<List<SupportClass>>(text, options);
                    List<StockOutDetail> trackList = new List<StockOutDetail>();
                    foreach (var item in readList)
                    {
                        List<Product> products = JsonSerializer.Deserialize<List<Product>>(item.detailProductList);
                        List<StockTracker> changes = JsonSerializer.Deserialize<List<StockTracker>>(item.detailStockChange);
                        StockOutDetail itemNew = new StockOutDetail
                        {
                            detailId = item.detailId,
                            detailDateAdded = item.detailDateAdded,
                            detailUser = item.detailUser,
                            detailProductList = products,
                            detailStockChange = changes
                        };
                        trackList.Add(itemNew);
                    }

                    return trackList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
