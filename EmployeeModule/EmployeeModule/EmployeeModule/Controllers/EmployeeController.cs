using EmployeeModule.Models;
using EmployeeModule.EmployeesRepository;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EmployeeModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository newEmployeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            newEmployeeRepository = employeeRepository;
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "Employee")]
        public IActionResult GetAllEmployees()
        {
            List<Employee> employees = newEmployeeRepository.GetAllEmployee();
            if (employees == null)
                return NotFound();
            return Ok(employees);
        }

        [HttpPost("[action]")]
        public IActionResult CheckCredentials([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                EmployeeResponse employee1 = newEmployeeRepository.GetEmployee(employee);
                if (employee1 == null)
                    return BadRequest(employee1);
                return Ok(employee1);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw e;
            }
        }
    }
}
