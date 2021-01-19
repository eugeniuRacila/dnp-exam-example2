using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using DNP_EXAM_EXAMPLE_2.Entities;
using DNP_EXAM_EXAMPLE_2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNP_EXAM_EXAMPLE_2.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        
        [HttpGet]
        [Route("employees/{employeeId:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int employeeId)
        {
            Employee foundEmployee = await _employeeService.GetEmployeeById(employeeId);

            if (foundEmployee == null)
                return NotFound();

            return foundEmployee;
        }

        [HttpGet]
        [Route("employees")]
        public ActionResult<List<Employee>> GetAllEmployees(bool? hasOvertime)
        {
            return _employeeService.FilterEmployeesBasedOnOvertime(_employeeService.GetAllEmployees(), hasOvertime);
        }

        [HttpGet]
        [Route("payments")]
        public ActionResult<List<Paycheck>> GetAllEmployeesPayments(bool? hasOvertime)
        {
            return _employeeService.GetEmployeesPayments(_employeeService.GetAllEmployees(), hasOvertime);
        }

        [HttpPost]
        [Route("employees")]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            Console.WriteLine("Create employee end-point");
            Console.WriteLine(JsonSerializer.Serialize(employee));
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest();
            // }

            await _employeeService.InsertEmployee(employee);

            return CreatedAtAction(nameof(GetEmployee), new { employeeId = employee.Id }, employee);
        }
    }
}