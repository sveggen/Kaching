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

        public List<PersonVM> GetPersons()
        {
            var persons = _personRepository.GetAllPersons();
            return _mapper.Map<List<PersonVM>>(persons);
        }

        public List<PersonVM> GetPersonsWithoutYourself(string username)
        {
            var persons = _personRepository.GetAllPersonsWithoutYourself(username);
            return _mapper.Map<List<PersonVM>>(persons);
        }

        public PersonVM GetPersonByUsername(string userName)
        {
            var person = _personRepository.GetPersonByUserName(userName);
            return _mapper.Map<PersonVM>(person);
        }
    }
}