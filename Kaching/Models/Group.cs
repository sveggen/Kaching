namespace Kaching.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public int MaxMembers { get; set; }

        public List<Person> Members { get; set; }

        public List<Expense> Expenses { get; set; }

        public DateTime Created { get; set; }
    }
}
