using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class PersonProfile : Profile
    {

        public PersonProfile()
        {
            CreateMap<Person, PersonVM>();
            CreateMap<PersonVM, Person>();
        }
    }
}
