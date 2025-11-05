using System.ComponentModel.DataAnnotations;

namespace Employee.MvcClient.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Location { get; set; }
    }
}
