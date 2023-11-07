using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Database.Entities;

namespace Manage_tasks_Biznes_Logic.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<TeamEntity, TeamDetailsForUserDto>()
            .ForMember(dto => dto.TeamId, o => o.MapFrom(t => t.Id))
            .ForMember(dto => dto.CanEditTeam, o => o.Ignore());

        CreateMap<TeamEntity, TeamBasicDto>()
            .ForMember(dto => dto.TeamId, o => o.MapFrom(t => t.Id))
            /*.ForMember(dto => dto.NumberOfMembers, o => o.MapFrom(t => t.Members.Count))*/;

        CreateMap<UserEntity, TeamMemberDto>()
            .ForMember(dto => dto.MemberId, o => o.MapFrom(u => u.Id));

        CreateMap<TeamAddDto, TeamEntity>()
            .ForMember(e => e.Members, o => o.MapFrom(_ => new List<UserEntity>()))
            .ForMember(e => e.Id, o => o.Ignore())
            .ForMember(e => e.LeaderId, o => o.Ignore())
            .ForMember(e => e.Leader, o => o.Ignore())
            .ForMember(e => e.Projects, o => o.Ignore());

        CreateMap<TeamNameEditDto, TeamEntity>()
            .ForMember(e => e.Id, o => o.Ignore())
            .ForMember(e => e.LeaderId, o => o.Ignore())
            .ForMember(e => e.Leader, o => o.Ignore())
            .ForMember(e => e.Members, o => o.Ignore())
            .ForMember(e => e.Projects, o => o.Ignore());
    }
}