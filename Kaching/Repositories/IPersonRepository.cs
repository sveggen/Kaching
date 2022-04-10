using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IPersonRepository : IDisposable
    {
        public void CreateNewPerson(Person person);

        public Person? GetPersonByUserId(string userId);

        public void DeletePersonByUserId(string userId);

        public void Save();
    }
}
