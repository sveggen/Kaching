using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.Models
{
    public class BaseExpense
    {
        public int BaseExpenseId { get; set; }
        
        public int CreatorId { get; set; }

        public Person? Creator { get; set; }

        public int CategoryId { get; set; }
        
        public Category? Category { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Frequency Frequency { get; set; }

        public List<Expense>? Expenses { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
    }
}
