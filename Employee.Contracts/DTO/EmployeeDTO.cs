

using System.ComponentModel.DataAnnotations;

namespace Employee.Contracts.DTO
{
    public class EmployeeDTO
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeAddress { get; set; }
    }
}
