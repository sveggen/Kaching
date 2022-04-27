using Kaching.Models;
using Kaching.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public List<Person> GetAllPersons()
        {
            return _context.Person
                .ToList();
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

        public Person? GetPersonByUserName(string userName)
        {
            return _context.Person
                .FirstOrDefault(p => p.ConnectedUserName == userName);
        }

        public Person? GetPersonByPersonId(int personId)
        {
            return _context.Person
                .FirstOrDefault(p => p.PersonId == personId);
        }
    }
}
