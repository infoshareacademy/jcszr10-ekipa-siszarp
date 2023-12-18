using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Task;
using WebTaskMaster.Models.Task;

namespace WebTaskMaster.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskBasicForUserDto, MyTasksListModel>();
    }
}