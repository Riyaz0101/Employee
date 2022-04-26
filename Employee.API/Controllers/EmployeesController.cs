using Employee.Contracts.DTO;
using Employee.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger logger;

        private readonly IEmployeeService employeeService;
        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            this.employeeService = employeeService;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            return await employeeService.GetEmployees();
        }

        [HttpGet("{EmployeeId}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployees(int EmployeeId)
        {
            return await employeeService.GetEmployees(EmployeeId);
        }

        [Produces("application/json")]
        [HttpPost]
        [Route("Add-Employee")]
        public async Task<IActionResult> AddEmployees([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                var response = await employeeService.AddEmployees(employeeDTO);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("Create employee unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Update-Employee")]
        public async Task<IActionResult> UpdateEmployees([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                var response = await employeeService.UpdateEmployees(employeeDTO);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError("update employee unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Delete-Employee")]
        public async Task<IActionResult> DeleteEmployees(int EmployeeId)
        {
            try
            {
                await employeeService.DeleteEmployees(EmployeeId);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError("Delete employee unsuccessful Request", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
