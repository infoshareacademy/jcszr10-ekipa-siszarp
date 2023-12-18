using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Project;
using WebTaskMaster.Models.Project;

namespace WebTaskMaster.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectBasicDto, ProjectBasicModel>();

        CreateMap<ProjectListForUserDto, ProjectIndexModel>();
    }
}