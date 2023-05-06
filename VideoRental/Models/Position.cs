using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class Position
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
