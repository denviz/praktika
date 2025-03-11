using System;
using System.Collections.Generic;

namespace praktika.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string EmpEmail { get; set; } = null!;

    public string EmpPhone { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? Fullname { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Specification> Specifications { get; set; } = new List<Specification>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
