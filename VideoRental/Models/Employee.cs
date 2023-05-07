using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public int? UserId { get; set; }

    public int? PositionId { get; set; }

    public int? StoreId { get; set; }

    public virtual Position? Position { get; set; }

    public virtual StoreLocation? Store { get; set; }

    public virtual User? User { get; set; }
}
