using System.Collections.Generic;
using System.Threading.Tasks;
using Employees.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employees.WebApi.Interfaces
{
    public interface IEmployees
    {
        Task<IEnumerable<EmployeesResponse>> GetEmployees();
    }
}