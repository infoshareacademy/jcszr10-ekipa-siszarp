using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Database.Entities;

namespace Manage_tasks_Biznes_Logic.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<TeamEntity, TeamDetailsDto>()
            .ForMember(dto => dto.TeamId, o => o.MapFrom(t => t.Id));

        CreateMap<TeamEntity, TeamBasicDto>()
            .ForMember(dto => dto.TeamId, o => o.MapFrom(t => t.Id))
            .ForMember(dto => dto.NumberOfMembers, o => o.MapFrom(t => t.Members.Count));

        CreateMap<UserEntity, TeamMemberDto>()
            .ForMember(dto => dto.MemberId, o => o.MapFrom(u => u.Id));
    }
}