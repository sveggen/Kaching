namespace Kaching.ViewModels
{
    public class GroupVm
    {
        public int GroupId { get; set; }

        public string? Name { get; set; }
        
        public string? Avatar { get; set; }
        
        public string? ColorCode { get; set; }

        public int MaxMembers { get; set; }
        
        public bool Personal { get; set; }

        public List<PersonLightVm>? Members { get; set; }

        public List<ExpenseVm>? Expenses { get; set; }
        
        public List<TransferVm>? Transfers { get; set; }
        
        public DateTime LastUpdated { get; set; }

        public DateTime Created { get; set; }
    }    
}
