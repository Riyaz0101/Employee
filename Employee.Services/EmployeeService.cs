using Employee.Contracts.DTO;
using Employee.DataAccess.GenericRepository.Interfaces;
using Employee.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.Infrastructure.Models;

namespace Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private const string SUCCESS_MESSAGE = "Employee Created Successfully";
        private const string COMPANYTYPE_SUCCESS = "Employee Saved Successfully";
        private const string COMPANYTYPE_NOTFOUNT = "Employee Type Not Fount";
        private readonly ILogger logger;
        private readonly IGenericRepository<Employees> genericRepository;
        public EmployeeService(IGenericRepository<Employees> genericRepository, ILogger<EmployeeService> logger)
        {
            this.genericRepository = genericRepository;
            this.logger = logger;
        }
        public async Task<string> AddEmployees(EmployeeDTO employeeDTO)
        {
            try
            {
                Employees employees = new()
                {
                    EmployeeName = employeeDTO.EmployeeName,
                    EmployeeAddress = employeeDTO.EmployeeAddress
                };

                await genericRepository.InsertAsync(employees);
                await genericRepository.SaveAsync();
                return SUCCESS_MESSAGE;
            }
            catch (Exception ex)
            {
                logger.LogError("Create employee Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task DeleteEmployees(int EmployeeId)
        {
            genericRepository.Delete(EmployeeId);
            await genericRepository.SaveAsync();
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            try
            {
                var response = await genericRepository.GetAsync();
                List<EmployeeDTO> listOfCompanyType = null;
                if (response.Any())
                {
                    listOfCompanyType = new List<EmployeeDTO>();
                    response.ToList().ForEach(emp => { EmployeeDTO employeeDTO = new() { EmployeeId = emp.EmployeeId, EmployeeName = emp.EmployeeName ,EmployeeAddress = emp.EmployeeAddress }; listOfCompanyType.Add(employeeDTO); });
                    return listOfCompanyType;
                }
                return listOfCompanyType;
            }
            catch (Exception ex)
            {
                logger.LogError("GetAll CompanyType Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<EmployeeDTO> GetEmployees(int EmployeeId)
        {
            try
            {
                var company = genericRepository.GetByID(EmployeeId);
                EmployeeDTO employeeDTO = null;
                if (company != null)
                {
                    employeeDTO = new()
                    {
                        EmployeeId = company.EmployeeId,
                        EmployeeName = company.EmployeeName,
                        EmployeeAddress = company.EmployeeAddress,

                    };
                    return await Task.Run(() => employeeDTO);
                }
                return await Task .Run(() => employeeDTO);
            }
            catch (Exception ex)
            {
                logger.LogError("Get employee Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<string> UpdateEmployees(EmployeeDTO employeeDTO)
        {
            try
            {
                var emp = genericRepository.GetByID(employeeDTO.EmployeeId);
                if (emp != null)
                {
                    emp.EmployeeAddress = employeeDTO.EmployeeAddress;
                    emp.EmployeeName = employeeDTO.EmployeeName;
                    genericRepository.Update(emp);
                    await genericRepository.SaveAsync();
                    return COMPANYTYPE_SUCCESS;
                }
                return COMPANYTYPE_NOTFOUNT;
            }
            catch (Exception ex)
            {
                logger.LogError("Update CompanyType Service unsuccessful Request", ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
