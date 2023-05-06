using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int FilmId { get; set; }

    public int CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsIssuied { get; set; }

    public int VideosInMediaId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Film Film { get; set; } = null!;
}
