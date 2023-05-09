using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoRental.Models;

public partial class Transaction : INotifyPropertyChanged
{
    private decimal _totalPrice;
    private bool _isIssuied;

    public int Id { get; set; }

    public int FilmId { get; set; }

    public int CustomerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsIssuied
    {
        get => _isIssuied;
        set
        {
            _isIssuied = value;
            OnPropertyChanged(nameof(IsIssuied));
            OnPropertyChanged(nameof(StatusText));
            OnPropertyChanged(nameof(StatusButtonText));
        }
    }

    public int VideosInMediaId { get; set; }

    public decimal TotalPrice
    {
        get => _totalPrice;
        set 
        {
            _totalPrice = value;
            OnPropertyChanged(nameof(TotalPrice));
        }
    }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Film Film { get; set; } = null!;

    public virtual FilmsInMedia VideosInMedia { get; set; } = null!;
    
    [NotMapped]
    public int RentCount { get; set; } = 1;

    public string StatusText => IsIssuied ? "Выдана" : "Не выдана";
    public string StatusButtonText => IsIssuied ? "Принять" : "Выдать";


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
