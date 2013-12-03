using System;
using Northwind.Model;

namespace Northwind.UI.Models
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public Address Address { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? ThisReportsToEmployeeId { get; set; }
        public string PhotoPath { get; set; }
    }
}