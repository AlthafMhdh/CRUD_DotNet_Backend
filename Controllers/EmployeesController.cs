using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeesController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _employeeDbContext.Employees.AddAsync(employee);
            await _employeeDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        } 
    
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployee)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Salary = updateEmployee.Salary;
            employee.PhoneNumber = updateEmployee.PhoneNumber;
            employee.Department = updateEmployee.Department;

            await _employeeDbContext.SaveChangesAsync();
            return Ok(employee);

        }  

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();
            return Ok(employee);

        } 
    }
}
