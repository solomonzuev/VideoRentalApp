using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class FilmCredit
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Sex { get; set; }

    public short? Height { get; set; }

    public DateTime? Birthdate { get; set; }

    public DateTime? Deathdate { get; set; }

    public virtual ICollection<Film> FilmAuthors { get; set; } = new List<Film>();

    public virtual ICollection<Film> FilmDirectors { get; set; } = new List<Film>();

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
