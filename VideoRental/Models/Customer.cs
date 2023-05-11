using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace VideoRental.Models;

public partial class Customer : INotifyPropertyChanged
{
    private bool _inBlackList;

    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public int? UserId { get; set; }

    public bool InBlackList
    {
        get => _inBlackList;
        set
        {
            _inBlackList = value;
            OnPropertyChanged(nameof(InBlackList));
            OnPropertyChanged(nameof(InBlackListText));
            OnPropertyChanged(nameof(ChangeBlackListText));
        }
    }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }


    public string InBlackListText => InBlackList ? "Да" : "Нет";
    public string ChangeBlackListText => InBlackList ? "Из ЧС" : "В ЧС";


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
