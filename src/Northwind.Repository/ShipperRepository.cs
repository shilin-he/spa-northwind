using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using Northwind.Model;

namespace Northwind.Repository
{
    public interface IShipperRepository : IDisposable
    {
        IEnumerable<Shipper> GetShippers();
        Shipper GetShipperById(int id);
        Shipper AddShipper(Shipper shipper);
        Shipper UpdateShipper(Shipper shipper);
        void DeleteShipper(int id);
    }

    public class ShipperRepository : IShipperRepository
    {
        private readonly NorthwindContext _ctx;

        public ShipperRepository()
        {
            _ctx = new NorthwindContext();
        }

        public IEnumerable<Shipper> GetShippers()
        {
            return _ctx.Shippers;
        }

        public Shipper GetShipperById(int id)
        {
            return _ctx.Shippers.Find(id);
        }

        public Shipper AddShipper(Shipper shipper)
        {
            _ctx.Shippers.Add(shipper);
            _ctx.SaveChanges();
            return shipper;
        }

        public Shipper UpdateShipper(Shipper shipper)
        {
            _ctx.Entry(shipper).State = EntityState.Modified;
            _ctx.SaveChanges();
            return shipper;
        }

        public void DeleteShipper(int id)
        {
            _ctx.Shippers.Remove(_ctx.Shippers.Find(id));
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}