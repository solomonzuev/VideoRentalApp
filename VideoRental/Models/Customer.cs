using System;
using System.Collections.Generic;

namespace VideoRental.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public int? UserId { get; set; }

    public bool InBlackList { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }


    public string InBlackListText => InBlackList ? "Да" : "Нет";
}
