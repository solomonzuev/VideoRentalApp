﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VideoRental.Models;

public partial class Transaction : INotifyPropertyChanged
{
    public int Id { get; set; }

    public int FilmId { get; set; }

    public int CustomerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsIssuied { get; set; }

    public int VideosInMediaId { get; set; }

    
    private decimal _totalPrice;

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

    public int RentCount { get; set; } = 1;


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
