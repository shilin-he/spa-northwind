using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.Model;
using Northwind.Repository;
using Northwind.UI.Models;

namespace Northwind.UI.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IAdapter _adapter;
        private readonly IEmployeeRepository _repo;

        public EmployeeController(IEmployeeRepository employeeRepository, IAdapter adapter)
        {
            _repo = employeeRepository;
            _adapter = adapter;
        }

        public EmployeeController()
        {
            _repo = new EmployeeRepository();
            _adapter = new Adapter();
        }

        public IEnumerable<EmployeeDto> Get()
        {
            return _adapter.Adapt<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(_repo.GetEmployees());
        }

        // GET api/employee/5
        public HttpResponseMessage Get(int id)
        {
            Employee employee = _repo.GetEmployeeById(id);
            return employee == null
                ? Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found")
                : Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Employee, EmployeeDto>(employee));
        }

        // POST api/employeeDto
        public HttpResponseMessage Post([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Employee employeeModel = _repo.AddEmployee(_adapter.Adapt<EmployeeDto, Employee>(employeeDto));

            HttpResponseMessage response = Request.CreateResponse(
                HttpStatusCode.Created, _adapter.Adapt<Employee, EmployeeDto>(employeeModel));
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new {id = employeeDto.EmployeeId}));

            return response;
        }

        // PUT api/employeeDto/5
        public HttpResponseMessage Put(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != employeeDto.EmployeeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Employee employee = _repo.UpdateEmployee(_adapter.Adapt<EmployeeDto, Employee>(employeeDto));

            return Request.CreateResponse(HttpStatusCode.OK, _adapter.Adapt<Employee, EmployeeDto>(employee));
        }

        // DELETE api/employee/5
        public HttpResponseMessage Delete(int id)
        {
            _repo.DeleteEmployee(id);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
            base.Dispose(disposing);
        }
    }
}