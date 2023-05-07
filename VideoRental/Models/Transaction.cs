using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int FilmId { get; set; }

    public int CustomerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsIssuied { get; set; }

    public int VideosInMediaId { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Film Film { get; set; } = null!;

    public virtual FilmsInMedia VideosInMedia { get; set; } = null!;
}
