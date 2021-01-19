using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeWeb.Entities;

namespace EmployeeWeb.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees(bool? hasOvertime);
        Task<Employee> InsertEmployee(Employee employeeToInsert);
    }
    
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Employee>> GetEmployees(bool? hasOvertime)
        {
            if (hasOvertime != null)
                return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employees?hasOvertime={hasOvertime}");

            return await _httpClient.GetFromJsonAsync<List<Employee>>("api/employees");
        }

        public async Task<Employee> InsertEmployee(Employee employeeToInsert)
        {
            StringContent content = new StringContent(JsonSerializer.Serialize(employeeToInsert), Encoding.UTF8, "application/json");
            
            var insertedEmployee = await _httpClient.PostAsync("api/employees", content);
            Employee e = JsonSerializer.Deserialize<Employee>(await insertedEmployee.Content.ReadAsStringAsync());
            Console.WriteLine(await insertedEmployee.Content.ReadAsStringAsync());
            Console.WriteLine(JsonSerializer.Serialize(e));
            
            return JsonSerializer.Deserialize<Employee>(await insertedEmployee.Content.ReadAsStringAsync());
        }
    }
}