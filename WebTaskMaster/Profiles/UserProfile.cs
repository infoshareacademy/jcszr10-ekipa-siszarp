using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.User;
using WebTaskMaster.Models.User;

namespace WebTaskMaster.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDetailsDto, UserDetailsModel>()
            .ReverseMap();
    }
}