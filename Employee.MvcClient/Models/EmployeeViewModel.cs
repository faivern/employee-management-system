using System.ComponentModel.DataAnnotations;

namespace Employee.MvcClient.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
    public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
  public decimal? Salary { get; set; }
        public int DepartmentId { get; set; }
        
// Department properties
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
    }
}
