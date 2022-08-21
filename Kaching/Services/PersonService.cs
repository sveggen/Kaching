using AutoMapper;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services
{
    public class PersonService : IPersonService
    {

        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public List<PersonLightVm> GetPersons()
        {
            var persons = _personRepository.GetAllPersons();
            return _mapper.Map<List<PersonLightVm>>(persons);
        }

        public List<PersonLightVm> GetPersonsWithoutYourself(string username)
        {
            var persons = _personRepository.GetAllPersonsWithoutYourself(username);
            return _mapper.Map<List<PersonLightVm>>(persons);
        }

        public PersonLightVm GetPersonByUsername(string userName)
        {
            var person = _personRepository.GetPersonByUserName(userName);
            return _mapper.Map<PersonLightVm>(person);
        }
    }
}