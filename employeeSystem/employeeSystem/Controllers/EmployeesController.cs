using Application.Services;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace employeeSystem.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {

        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null) { 
            
                return NotFound();
            }

            return Ok(employee);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] EmployeeDto employee)
        {
            var employeeAdd = new Employee
            {
                Name = employee.Name,
                Position = employee.Position,
                Office = employee.Office,
                Salary = employee.Salary,
                Age = employee.Age
            };

            await _employeeService.AddEmployeeAsync(employeeAdd);
            return CreatedAtAction(nameof(GetById),new {id = employee.Id},employee);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> update(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id) {

                return BadRequest();

            }


            await _employeeService.UpdateEmployeeAsync(employee);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){

            await _employeeService.DeleteEmployeeAsync(id);

            return NoContent();

        }



    }
}
