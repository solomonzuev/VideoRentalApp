using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class MediaType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FilmsInMedia> FilmsInMedia { get; set; } = new List<FilmsInMedia>();
}
