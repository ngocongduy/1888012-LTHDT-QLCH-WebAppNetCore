using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Models
{
    public class StockTracker
    {
        public int stockId { get; set; }
        public int stockChange { get; set; }
    }
    public abstract class StockTrackDetail
    {
        public int detailId { get; set; }
        public string detailUser { get; set; }
        public DateTime detailDateAdded { get; set; }
        public List<Product> detailProductList { get; set; }
        public List<StockTracker> detailStockChange { get; set; }
    }

    public class StockInDetail : StockTrackDetail
    {

    }

    public class StockOutDetail : StockTrackDetail
    {
    }
    public interface IStockTrack
    {
        public int detailId { get; set; }
        public string detailUser { get; set; }
        public DateTime detailDateAdded { get; set; }
        public List<Product> detailProductList { get; set; }
        public List<StockTracker> detailStockChange { get; set; }
    }

    public class StockIn : IStockTrack
    {
        public int detailId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string detailUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime detailDateAdded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Product> detailProductList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<StockTracker> detailStockChange { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
    public class StockOut : IStockTrack
    {
        public int detailId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string detailUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime detailDateAdded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Product> detailProductList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<StockTracker> detailStockChange { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }




}
