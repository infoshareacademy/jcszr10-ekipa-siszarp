using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Account;
using WebTaskMaster.Models.Account;

namespace WebTaskMaster.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<RegisterModel, RegisterDto>();

        CreateMap<RegisterResultDto, RegisterModel>()
            .ForMember(m => m.FirstName, o => o.Ignore())
            .ForMember(m => m.LastName, o => o.Ignore())
            .ForMember(m => m.Email, o => o.Ignore())
            .ForMember(m => m.Password, o => o.Ignore())
            .ForMember(m => m.ConfirmPassword, o => o.Ignore());

        CreateMap<LoginModel, LoginDto>();

        CreateMap<DeleteAccountModel, DeleteAccountDto>();

        CreateMap<EditEmailModel, EditEmailDto>();

        CreateMap<EditEmailResultDto, EditEmailModel>()
            .ForMember(m => m.UserId, o => o.Ignore())
            .ForMember(m => m.CurrentEmail, o => o.Ignore())
            .ForMember(m => m.NewEmail, o => o.Ignore());

        CreateMap<EditPasswordModel, EditPasswordDto>();

        CreateMap<EditPasswordResultDto, EditPasswordModel>()
            .ForMember(m => m.UserId, o => o.Ignore())
            .ForMember(m => m.CurrentPassword, o => o.Ignore())
            .ForMember(m => m.NewPassword, o => o.Ignore())
            .ForMember(m => m.ConfirmNewPassword, o => o.Ignore());
    }
}