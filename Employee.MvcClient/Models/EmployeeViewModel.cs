using System.ComponentModel.DataAnnotations;

namespace Employee.MvcClient.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        
        [Required]
        public string FirstName { get; set; } = null!;
        
        [Required]
        public string LastName { get; set; } = null!;
        
        [EmailAddress]
        public string? Email { get; set; }
        
        [Range(1, 200_000, ErrorMessage = "Please select salary between 1 - 200,000")]
        public decimal? Salary { get; set; }
     
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid department ID.")]
        public int DepartmentId { get; set; }
        
        // Department properties (not validated, for display only)
        public string? Name { get; set; }
        public string? Location { get; set; }
    }
}
