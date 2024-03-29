﻿using System.ComponentModel;

namespace Kaching.ViewModels;

public class PersonVm
{
    public int PersonId { get; set; }

    [DisplayName("Username")]
    public string? UserName { get; set; }
        
    public string? Avatar { get; set; }

    public string? ColorCode { get; set; }
    
    public List<TransferVm>? Transfers { get; set; }
    
    public List<ExpenseVm>? Expenses { get; set; }
}