using EmployeeLogix.Server.Reposatories;
using EmployeeLogix.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLogix.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _employeeRepository;

        public EmployeeController(IEmployeeRepo employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var result = await _employeeRepository.GetAllEmployees();
            return Ok(result);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var Employee = await _employeeRepository.FindEmployeeById(id);
            return Ok(Employee);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeController>/5
        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody]Employee employee)
        {
            var result = await _employeeRepository.EditEmployee(employee);
            return Ok(result);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee (Guid id)
        {
            await _employeeRepository.DeleteEmployeeById(id);
            return Ok(200);
        }
    }
}
