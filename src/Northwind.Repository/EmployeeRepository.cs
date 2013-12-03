using System;
using System.Collections.Generic;
using System.Data;
using Northwind.Model;

namespace Northwind.Repository
{
    public interface IEmployeeRepository : IDisposable
    {
        Employee GetEmployeeById(int id);
        IEnumerable<Employee> GetEmployees();
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NorthwindContext _ctx;

        public EmployeeRepository()
        {
            _ctx = new NorthwindContext();
        }

        public Employee GetEmployeeById(int id)
        {
            return _ctx.Employees.Find(id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _ctx.Employees;
        }

        public Employee AddEmployee(Employee employee)
        {
            _ctx.Employees.Add(employee);
            _ctx.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            _ctx.Entry(employee).State = EntityState.Modified;
            _ctx.SaveChanges();
            return employee;
        }

        public void DeleteEmployee(int id)
        {
            _ctx.Employees.Remove(_ctx.Employees.Find(id));
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}