using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Backend.Data;

public partial class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [Column(TypeName = "decimal(20, 0)")]
    public decimal? Salary { get; set; }

    public int DepartmentId { get; set; }
}
