using System.Text.RegularExpressions;
using AutoMapper;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<Group, GroupVm>();
        CreateMap<GroupCreateVm, Group>();
    }
}