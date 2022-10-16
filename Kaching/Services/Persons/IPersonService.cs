using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IPersonService
    {
        public PersonLightVm GetPersonByUsername(string userName);

        public List<PersonLightVm> GetPersons();
        
        public List<PersonLightVm> GetPersonsInGroup(int groupId);

        public List<PersonLightVm> GetPersonsWithoutYourself(string username);
    }
}
