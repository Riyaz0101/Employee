using Employee.API.Controllers;
using Employee.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Moq;
using Employee.Contracts.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Test
{
    public class EmpTest
    {
        #region Private Variables
        private readonly Mock<IEmployeeService> _employeeService;
        private readonly EmployeesController _employeesController;
        private readonly Mock<ILogger<EmployeesController>> _logger;

        public EmpTest()
        {
            _employeeService = new Mock<IEmployeeService>();
            _logger = new Mock<ILogger<EmployeesController>>();
            _employeesController = new EmployeesController(_employeeService.Object, _logger.Object);
        }
        #endregion

        #region Mock Objects
        List<EmployeeDTO> companyTypeList = new()
        {
            new EmployeeDTO()
            {
                EmployeeId = 1,
                EmployeeName = "Riyaz",
                EmployeeAddress = "Chennai"
            },
            new EmployeeDTO()
            {
                EmployeeId = 2,
                EmployeeName = "AAA",
                EmployeeAddress= "Pattukkottai"
            },
            new EmployeeDTO()
            {
                EmployeeId = 3,
                EmployeeName = "BBB",
                EmployeeAddress = "aaa"
            }
        };
        #endregion

        [Fact]
        public async Task GetAllEmployeeTest()
        {
            try
            {
                //Act
                _employeeService.Setup(f => f.GetEmployees()).ReturnsAsync(companyTypeList);
                var output = await _employeesController.GetEmployees();

                //Arrange Assert
                Assert.NotNull(output);
            }
            catch (Exception ex)
            {
                Assert.False(false, ex.Message);
            }
        }
    }
}
