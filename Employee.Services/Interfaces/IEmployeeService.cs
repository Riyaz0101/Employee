using Employee.Contracts.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetEmployees();
        Task<EmployeeDTO> GetEmployees(int EmployeeId);
        Task<string> AddEmployees(EmployeeDTO employeeDTO);
        Task<string> UpdateEmployees(EmployeeDTO employeeDTO);
        Task DeleteEmployees(int EmployeeId);
    }
}
