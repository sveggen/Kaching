﻿using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class PersonProfile : Profile
    {

        public PersonProfile()
        {
            CreateMap<Person, PersonLightVm>();
            CreateMap<PersonLightVm, Person>();

            CreateMap<Person, PersonAdminVm>();

            CreateMap<Person, PersonVm>();
        }
    }
}
