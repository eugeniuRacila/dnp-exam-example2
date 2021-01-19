using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNP_EXAM_EXAMPLE_2.DataAccess;
using DNP_EXAM_EXAMPLE_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace DNP_EXAM_EXAMPLE_2.Services
{
    public interface IEmployeeService
    {
        List<Employee> FilterEmployeesBasedOnOvertime(List<Employee> employees, bool? hasOvertime);
        double GetTotalMonthlyExpense(List<Employee> employees);

        Task<Employee> GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        List<Paycheck> GetEmployeesPayments(List<Employee> employees, bool? hasOvertime);
        Task InsertEmployee(Employee employee);
    }
    
    public class EmployeeService : IEmployeeService
    {
        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                return await dbContext.Employees.FirstAsync(e => e.Id ==  employeeId);
            }
        }
        
        public List<Employee> GetAllEmployees()
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                return dbContext.Employees.ToList();
            }
        }
        
        public List<Employee> FilterEmployeesBasedOnOvertime(List<Employee> employees, bool? hasOvertime)
        {
            if (hasOvertime != null)
            {
                // Return the employees which only worked overtime
                if (hasOvertime == true)
                {
                    return employees.FindAll(e => e.HoursPerMonth > 150);
                }
                
                // Return the employees which did not work overtime
                return employees.FindAll(e => e.HoursPerMonth <= 150);
            }

            // Return back the given employees
            return employees;
        }

        public List<Paycheck> GetEmployeesPayments(List<Employee> employees, bool? hasOvertime)
        {
            // Return paychecks of the filtered employees
            if (hasOvertime != null)
            {
                return FilterEmployeesBasedOnOvertime(employees, hasOvertime).Select(e => new Paycheck { Employee = e, Amount = e.GetMonthlyPay()}).ToList();
            }

            return employees.Select(e => new Paycheck { Employee = e, Amount = e.GetMonthlyPay()}).ToList();
        }

        public double GetTotalMonthlyExpense(List<Employee> employees)
        {
            return employees.Aggregate(0.0, (s, e) => s + e.GetMonthlyPay());
        }

        public async Task InsertEmployee(Employee employee)
        {
            using (EmployeeDBContext dbContext = new EmployeeDBContext())
            {
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}