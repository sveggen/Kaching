using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IPersonRepository : IDisposable
    {
        public void CreateNewPerson(Person person);

        public Person? GetPersonByUserId(string userId);

        public Person? GetPersonByUserName(string userName);

        public List<Person> GetAllPersons();

        public void DeletePersonByUserId(string userId);

        public void Save();
    }
}
