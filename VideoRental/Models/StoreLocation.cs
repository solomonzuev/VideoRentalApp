using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class StoreLocation
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public TimeSpan OpeningTime { get; set; }

    public TimeSpan ClosingTime { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<FilmsInMedium> FilmsInMedia { get; set; } = new List<FilmsInMedium>();
}
