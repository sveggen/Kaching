using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IPersonRepository : IDisposable
    {
        public void CreateNewPerson(Person person);

        public Person? GetPersonByUserId(string userId);

        public Person? GetPersonByPersonId(int personId);

        public Person? GetPersonByUserName(string userName);

        public List<Person> GetAllPersonsWithoutYourself(string username);

        public List<Person> GetAllPersons();
        
        public List<Person> GetAllPersonsInGroup(int groupId);

        public List<Person> GetPersonsForSettlement(int groupId);

        public void DeletePersonByUserId(string userId);

        public void Save();
    }
}
