using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Employees.WebApi.Interfaces;
using Employees.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Employees.WebApi.Facades{
    public class EmployeesFacade : IEmployees
    {
        public Task<IEnumerable<EmployeesResponse>> GetEmployees()
        {
            var client = new HttpClient();
            var uri = "http://masglobaltestapi.azurewebsites.net/api/Employees";
            var employeeList = new List<EmployeesResponse>();
            var response = client.GetAsync(uri)
                         .ContinueWith((taskresponse) =>
                         {
                             var resp = taskresponse.Result;
                             var jsonString = resp.Content.ReadAsStringAsync();
                             jsonString.Wait();
                             employeeList = JsonConvert.DeserializeObject<List<EmployeesResponse>>(jsonString.Result);
                         }); ;
            response.Wait();

            CalculateAnnualSalary(employeeList);
            return Task.FromResult<IEnumerable<EmployeesResponse>>(employeeList);
        }

        private static void CalculateAnnualSalary(List<EmployeesResponse> employeeList)
        {
            foreach (EmployeesResponse emp in employeeList)
            {

                switch (emp.ContractTypeName)
                {
                    case "HourlySalaryEmployee":
                        emp.AnnualSalary = 120 * 12 * emp.HourlySalary;
                        break;

                    case "MonthlySalaryEmployee":
                        emp.AnnualSalary = emp.MonthlySalary * 12;
                        break;

                    default:
                        emp.AnnualSalary = 0;
                        break;
                }

            }
        }
    }
}