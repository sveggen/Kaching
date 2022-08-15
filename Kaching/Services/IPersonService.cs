using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IPersonService
    {
        public PersonVM GetPersonByUsername(string userName);

        public List<PersonVM> GetPersons();

        public List<PersonVM> GetPersonsWithoutYourself(string username);
    }
}
