using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class VideosInMedia
{
    public int Id { get; set; }

    public int VideoId { get; set; }

    public int MediaTypeId { get; set; }

    public int Units { get; set; }

    public int StoreId { get; set; }

    public bool? IsAvaliable { get; set; }

    public virtual MediaType MediaType { get; set; } = null!;

    public virtual StoreLocation Store { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
