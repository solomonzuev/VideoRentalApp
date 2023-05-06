using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class Video
{
    public int Id { get; set; }

    public string VideoName { get; set; } = null!;

    public int GenreId { get; set; }

    public int AuthorId { get; set; }

    public int DirectorId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public byte LimitAge { get; set; }

    public decimal Price3Days { get; set; }

    public virtual VideoCredit Author { get; set; } = null!;

    public virtual VideoCredit Director { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<VideosInMedia> VideosInMedia { get; set; } = new List<VideosInMedia>();

    public virtual ICollection<VideoCredit> Actors { get; set; } = new List<VideoCredit>();
}
