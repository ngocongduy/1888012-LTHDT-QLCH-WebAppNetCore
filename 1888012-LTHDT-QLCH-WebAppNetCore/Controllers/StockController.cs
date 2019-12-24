using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _1888012_LTHDT_QLCH_WebAppNetCore.DAL;
using _1888012_LTHDT_QLCH_WebAppNetCore.Models;
using _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Controllers
{
    public class StockController : Controller
    {
        const string folder = "data";
        const string productFile = "product.json";
        const string stockInFile = "stock_in.json";
        const string stockOutFile = "stock_out.json";
        private readonly List<StockInDetail> inList;
        private readonly List<StockOutDetail> outList;
        private readonly List<Product> products;
        private readonly string rootPath;

        //Only use available DI service for IWebHostEnvironment
        public StockController(IWebHostEnvironment webHostEnvironment)
        {
            rootPath = Path.Combine(webHostEnvironment.WebRootPath, folder);
            inList = LocalDataAccess.ReadDataStockIn(rootPath, stockInFile);
            outList = LocalDataAccess.ReadDataStockOut(rootPath, stockOutFile);
            products = LocalDataAccess.ReadDataProduct(rootPath, productFile);
        }
        //[HttpGet]
        public IActionResult StockIn()
        {
            StockInViewModel model = new StockInViewModel();
            model.detailProductList = products;
            model.inList = inList;
            return View(model);
        }

        public IActionResult StockOut()
        {
            StockOutViewModel model = new StockOutViewModel();
            model.detailProductList = products;
            model.outList = outList;
            return View(model);
        }

        [HttpPost]
        public void AddStockIn(int[] selectedIds, int[] selectedValues, string user)
        {
            StockInDetail note = new StockInDetail();
            note.detailProductList = new List<Product>();

            var newList = new List<StockInDetail>();
            if (inList == null )
            {
                note.detailId = 1;
                
            }
            else
            {
                note.detailId = inList.Last().detailId + 1;
                newList = new List<StockInDetail>(inList);
            }

            //Assign value for new note
            note.detailUser = user;
            note.detailDateAdded = DateTime.Now;
            note.detailProductList = new List<Product>();
            note.detailStockChange = new List<StockTracker>();
            List<Product> changedProducts = new List<Product>();
            (note.detailProductList,note.detailStockChange, changedProducts) = UpdateNoteProductList(selectedIds, selectedValues);
            if (note.detailProductList != null && note.detailStockChange != null && changedProducts != null)
            {
                newList.Add(note);
                //Update database
                UpdateProductList(changedProducts);
                LocalDataAccess.WriteDataStockIn(rootPath, stockInFile, newList);
            }
            
        }             
        [HttpPost]
        public void DeleteStockIn(int selectedId)
        {
            var newList = new List<StockInDetail>(inList);
            var note = newList.Find(n => n.detailId == selectedId);
            if (note != null)
            {
                //Roll back
                bool isRolledBack = RolledBack(note.detailProductList, note.detailStockChange);
                if (isRolledBack)
                {
                    newList.Remove(note);
                    LocalDataAccess.WriteDataStockIn(rootPath, stockInFile, newList);
                }                       
            }
        }
        [HttpPost]
        public void UpdateStockIn(int selectedId, int[] selectedIds, int[] selectedValues, string user)
        {
            var newList = new List<StockInDetail>(inList);
            var index = newList.FindIndex(n => n.detailId == selectedId);

            if (index >= 0)
            {
                var note = newList[index];
                note.detailUser = user;
                note.detailDateAdded = DateTime.Now;
                
                if (selectedIds.Length > 0)
                {
                    //Roll back
                    bool isRolledBack = RolledBack(note.detailProductList, note.detailStockChange);

                    //New status
                    if (isRolledBack)
                    {
                        note.detailProductList.Clear();
                        note.detailStockChange.Clear();
                        List<Product> changedProducts = new List<Product>();
                        (note.detailProductList, note.detailStockChange, changedProducts) = UpdateNoteProductList(selectedIds, selectedValues);
                        if (note.detailProductList != null && note.detailStockChange != null && changedProducts != null)
                        {
                            UpdateProductList(note.detailProductList);
                            LocalDataAccess.WriteDataStockIn(rootPath, stockInFile, newList);
                        }
                    }
                                       
                }

            }
        }

        [HttpPost]
        public void AddStockOut(int[] selectedIds, int[] selectedValues, string user)
        {
            StockOutDetail note = new StockOutDetail();
            note.detailProductList = new List<Product>();

            var newList = new List<StockOutDetail>();
            if (outList == null)
            {
                note.detailId = 1;

            }
            else
            {
                note.detailId = outList.Last().detailId + 1;
                newList = new List<StockOutDetail>(outList);
            }

            //Assign value for new note
            note.detailUser = user;
            note.detailDateAdded = DateTime.Now;
            note.detailProductList = new List<Product>();
            note.detailStockChange = new List<StockTracker>();
            List<Product> changedProducts = new List<Product>();
            (note.detailProductList, note.detailStockChange, changedProducts) = UpdateNoteProductList(selectedIds, selectedValues, isPlus: false);
            if (note.detailProductList != null && note.detailStockChange != null && changedProducts != null)
            {
                newList.Add(note);
                //Update database
                UpdateProductList(changedProducts);
                LocalDataAccess.WriteDataStockOut(rootPath, stockOutFile, newList);
            }

        }
        [HttpPost]
        public void DeleteStockOut(int selectedId)
        {
            var newList = new List<StockOutDetail>(outList);
            var note = newList.Find(n => n.detailId == selectedId);
            if (note != null)
            {
                //Roll back
                bool isRolledBack = RolledBack(note.detailProductList, note.detailStockChange, isPlus: false);
                if (isRolledBack)
                {
                    newList.Remove(note);
                    LocalDataAccess.WriteDataStockOut(rootPath, stockOutFile, newList);
                }
            }
        }
        [HttpPost]
        public void UpdateStockOut(int selectedId, int[] selectedIds, int[] selectedValues, string user)
        {
            var newList = new List<StockOutDetail>(outList);
            var index = newList.FindIndex(n => n.detailId == selectedId);

            if (index >= 0)
            {
                var note = newList[index];
                note.detailUser = user;
                note.detailDateAdded = DateTime.Now;

                if (selectedIds.Length > 0)
                {
                    //Roll back
                    bool isRolledBack = RolledBack(note.detailProductList, note.detailStockChange,isPlus: false);

                    //New status
                    if (isRolledBack)
                    {
                        note.detailProductList.Clear();
                        note.detailStockChange.Clear();
                        List<Product> changedProducts = new List<Product>();
                        (note.detailProductList, note.detailStockChange, changedProducts) = UpdateNoteProductList(selectedIds, selectedValues, isPlus:false);
                        if (note.detailProductList != null && note.detailStockChange != null && changedProducts != null)
                        {
                            UpdateProductList(note.detailProductList);
                            LocalDataAccess.WriteDataStockOut(rootPath, stockOutFile, newList);
                        }
                    }

                }

            }
        }

        //Method to update database regarding products which has stock property changed
        private bool UpdateProductList(List<Product> changedProducts)
        {
            if (changedProducts.Count > 0)
            {
                var updatedProducts = new List<Product>(products); //Make a copy
                foreach (var product in changedProducts)
                {
                    int index = updatedProducts.FindIndex(p => p.Id == product.Id);
                    if (index >= 0)
                    {
                        updatedProducts[index] = product;
                    }

                }
                LocalDataAccess.WriteDataProduct(rootPath, productFile, updatedProducts);
                return true;
            }
            return false;
        }
        //Method to get a list of products which are add to a new note
        private (List<Product>, List<StockTracker>, List<Product>) UpdateNoteProductList(int[] selectedIds, int[] selectedValues, bool isPlus = true)
        {
            if (selectedIds.Length > 0)
            {
                List<Product> changedProducts = new List<Product>();
                List<Product> noteProducts = new List<Product>();
                List<StockTracker> stockChange = new List<StockTracker>();

                for (int i = 0; i < selectedIds.Length; i++)
                {
                    var index = products.FindIndex(p => p.Id == selectedIds[i]);
                    if (index >= 0)
                    {
                        Product product = products[index];
                        //Add products with original stock into note
                        noteProducts.Add(product);
                        if (isPlus)
                        {
                            product.Stock += selectedValues[i];
                        }
                        else
                        {
                            product.Stock -= selectedValues[i];
                        }
                        //Get a list of products with stock changed
                        changedProducts.Add(product);
                        stockChange.Add(new StockTracker { stockId = selectedIds[i], stockChange = selectedValues[i] });
                    }
                }

                return (noteProducts, stockChange, changedProducts);
            }
            return (null, null, null);
        }

        //Method to roll back when update or delete
        private bool RolledBack(List<Product> oldNoteProductList, List<StockTracker> oldNoteStockChange, bool isPlus = true)
        {
            //change in this controller means increase
            var currentProducts = new List<Product>(products); // Make a copy not reference
            foreach (var change in oldNoteStockChange)
            {
                var product = oldNoteProductList.Find(p => p.Id == change.stockId);
                int idx = currentProducts.FindIndex(p => p.Id == product.Id);
                if (isPlus)
                {
                    currentProducts[idx].Stock -= change.stockChange;
                }
                else
                {
                    currentProducts[idx].Stock += change.stockChange;
                }

            }
            return UpdateProductList(currentProducts);
        }
    }
}