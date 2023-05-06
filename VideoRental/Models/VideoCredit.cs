using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class VideoCredit
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Sex { get; set; }

    public short? Height { get; set; }

    public DateTime? Birthdate { get; set; }

    public DateTime? Deathdate { get; set; }

    public virtual ICollection<Video> VideoAuthors { get; set; } = new List<Video>();

    public virtual ICollection<Video> VideoDirectors { get; set; } = new List<Video>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
