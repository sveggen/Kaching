namespace Kaching.Enums
{
    public class Category
    {
        public int CategoryId { get; set;}

        public string? Name { get; set;}

        public List<BaseExpense>? BaseExpenses { get; set;}
    }
}