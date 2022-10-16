using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<Group, GroupVm>();
        CreateMap<GroupVm, Group>();
        CreateMap<GroupCreateVm, Group>()
            .ForMember(
                d => d.Members, 
                o => o.MapFrom(
                    s => s.Members.Select(detail => detail.PersonId).ToList()
                )
            );
        CreateMap<Group, SettlementVm>();
    }
}