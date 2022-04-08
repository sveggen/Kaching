using Kaching.Models;

namespace Kaching.Data
{
    public class PersonStore : IPersonStore
    {

        private DataContext _context;

        public PersonStore(DataContext context)
        {
            _context = context;
        }

        public void CreateNewPerson(Person person)
        {
            _context.Person.Add(person);
            _context.SaveChanges();
        }
    }

}
