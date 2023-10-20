using Manage_tasks_Biznes_Logic.Dtos.User;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public interface IUserService
{
    Task<User?> GetUserById(Guid id);
    Task<List<User>> GetAllUsers();
    Task<UserDetailsDto> GetUserDetails(Guid userId);
    Task EditUserDetails(UserDetailsDto dto);
}