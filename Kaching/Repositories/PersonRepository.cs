using Kaching.Models;
using Kaching.Data;

namespace Kaching.Repositories
{
    public class PersonRepository : IPersonRepository, IDisposable
    {

        private readonly DataContext _context;

        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateNewPerson(Person person)
        {
            _context.Person.Add(person);
            _context.SaveChanges();
        }

        public Person? GetPersonByUserId(string userId)
        {
             return _context.Person
                .FirstOrDefault(p => p.ConnectedUserId == userId);
        }

        private void DeletePerson(Person person)
        {
            _context.Person
                .Remove(person);
            _context.SaveChanges();
        }

        public void DeletePersonByUserId(string userId)
        {
            Person? person = GetPersonByUserId(userId);
            
            if (person != null)
            {
                DeletePerson(person);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
