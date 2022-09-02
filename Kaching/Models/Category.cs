namespace Kaching.Models
{
    public class Category
    {
        public int CategoryId { get; set;}

        public string? Name { get; set;}
        
        public string? Icon { get; set; }

        public List<BaseExpense>? BaseExpenses { get; set;}
    }
}