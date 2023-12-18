using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Project;
using Manage_tasks_Database.Entities;

namespace Manage_tasks_Biznes_Logic.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectEntity, ProjectBasicDto>()
            .ForMember(dto => dto.ProjectId, o => o.MapFrom(entity => entity.Id))
            .ForMember(dto => dto.CompletionPercent, o => o.Ignore());
    }
}