using AutoMapper;
using Kaching.Enums;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class PersonProfile : Profile
    {

        public PersonProfile()
        {
            CreateMap<Person, PersonLightVm>();
            CreateMap<PersonLightVm, Person>();
        }
    }
}
