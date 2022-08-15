namespace Kaching.Models
{
    public class Category2
    {
        public int CategoryId { get; set;}

        public string Name { get; set;}

        public List<Expense> Expenses { get; set;}
    }
}
