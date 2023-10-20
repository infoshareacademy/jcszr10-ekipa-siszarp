using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Team;
using WebTaskMaster.Models.Team;

namespace WebTaskMaster.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<TeamDetailsDto, TeamDetailsModel>();

        CreateMap<TeamMemberDto, TeamMemberModel>();

        CreateMap<TeamBasicDto, TeamEditModel>();

        CreateMap<TeamEditModel, TeamNameEditDto>();

        CreateMap<ICollection<TeamMemberDto>, TeamChangeLeaderModel>()
            .ForMember(m => m.AvailableLeaders, o => o.MapFrom(dto => dto))
            .ForMember(m => m.TeamId, o => o.Ignore())
            .ForMember(m => m.NewLeaderId, o => o.Ignore());

        CreateMap<TeamChangeLeaderModel, TeamChangeLeaderDto>();

        CreateMap<ICollection<TeamMemberDto>, TeamRemoveMembersModel>()
            .ForMember(m => m.AvailableRemoveMembers, o => o.MapFrom(dto => dto))
            .ForMember(m => m.TeamId, o => o.Ignore())
            .ForMember(m => m.RemoveMemberIds, o => o.Ignore());

        CreateMap<TeamRemoveMembersModel, TeamRemoveMembersDto>();

        CreateMap<ICollection<TeamMemberDto>, TeamAddMembersModel>()
            .ForMember(m => m.AvailableMembers, o => o.MapFrom(dto => dto))
            .ForMember(m => m.TeamId, o => o.Ignore())
            .ForMember(m => m.NewMemberIds, o => o.Ignore()); ;

        CreateMap<TeamAddMembersModel, TeamAddMembersDto>();

        CreateMap<TeamAddModel, TeamAddDto>();
    }
}