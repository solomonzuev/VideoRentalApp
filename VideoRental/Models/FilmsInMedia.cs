using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class FilmsInMedia
{
    public int Id { get; set; }

    public int FilmId { get; set; }

    public int MediaTypeId { get; set; }

    public int Units { get; set; }

    public int StoreId { get; set; }

    public bool? IsAvaliable { get; set; }

    public virtual Film Film { get; set; } = null!;

    public virtual MediaType MediaType { get; set; } = null!;

    public virtual StoreLocation Store { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
