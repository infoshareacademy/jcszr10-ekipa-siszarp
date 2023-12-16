using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Task;
using Manage_tasks_Database.Entities;

namespace Manage_tasks_Biznes_Logic.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskBasicForUserDto>()
            .ForMember(dto => dto.TaskId,
            o => o.MapFrom(e => e.Id))
            .ForMember(dto => dto.TaskName,
            o => o.MapFrom(e => e.Name))
            .ForMember(dto => dto.TaskDescription,
            o => o.MapFrom(e => e.Description))
            .ForMember(dto => dto.ProjectId,
            o => o.MapFrom(e => e.TaskList!.ProjectId))
            .ForMember(dto => dto.ProjectName,
            o => o.MapFrom(e => e.TaskList!.Project.Name))
            .ForMember(dto => dto.TaskStatus,
            o => o.MapFrom(e => e.StatusId.ToString()));
    }
}
