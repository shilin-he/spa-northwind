using System.Collections.Generic;

namespace Northwind.Model
{
    public sealed class Shipper
    {
        public Shipper()
        {
            Orders = new List<Order>();
        }

        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
