using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace VideoRental.Models;

public partial class Film : INotifyPropertyChanged
{
    private ICollection<FilmCredit> _actors = new List<FilmCredit>();
    private byte _limitAge;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int GenreId { get; set; }

    public int AuthorId { get; set; }

    public int DirectorId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public byte LimitAge
    {
        get => _limitAge;
        set
        {
            _limitAge = value;
            OnPropertyChanged(nameof(LimitAge));
        }
    }

    public decimal Price3Days { get; set; }

    public virtual FilmCredit Author { get; set; } = null!;

    public virtual FilmCredit Director { get; set; } = null!;

    public virtual ICollection<FilmsInMedia> FilmsInMedia { get; set; } = new List<FilmsInMedia>();

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<FilmCredit> Actors
    {
        get => _actors;
        set
        {
            _actors = value;
            OnPropertyChanged(nameof(Actors));
        }
    }
    public string ActorsText => string.Join(", ", Actors.Select(a => a.FullName));


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
