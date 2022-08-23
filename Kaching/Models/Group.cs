namespace Kaching.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string? Name { get; set; }
        
        public string? Avatar { get; set; }
        
        public string? ColorCode { get; set; }

        public int MaxMembers { get; set; }

        public List<Person>? Members { get; set; }

        public List<Expense>? Expenses { get; set; }
        
        public List<Transfer>? Transfers { get; set; }
        
        public DateTime LastUpdated { get; set; }

        public DateTime Created { get; set; }
    }
}
