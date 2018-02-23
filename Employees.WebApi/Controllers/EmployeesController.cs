using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.WebApi.Const;
using Employees.WebApi.Interfaces;
using Employees.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.WebApi.Controllers
{
    [Route(ServiceRoutes.Employees)]
    public class EmployeesController : Controller
    {
        private readonly IEmployees employees;
        public EmployeesController(IEmployees _employees){
            employees=_employees?? 
                throw new ArgumentNullException(nameof(_employees));
        }
        
        [HttpGet]
        public Task<IEnumerable<EmployeesResponse>> GetEmployees()
        {       
            return employees.GetEmployees();           
        }

        
        [HttpGet("{id}")]
        public Task<EmployeesResponse> GetEmployeesById(int id)
        {
            var emplist=employees.GetEmployees().Result.ToList();
            return Task.FromResult<EmployeesResponse>
                           (emplist.Where(e=>e.Id==id).FirstOrDefault());
        }
    }
}
