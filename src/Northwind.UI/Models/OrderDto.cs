using System;
using Northwind.Model;

namespace Northwind.UI.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerCompanyName { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? ShipVia { get; set; }
        public string ShipperCompanyName { get; set; }
    }
}