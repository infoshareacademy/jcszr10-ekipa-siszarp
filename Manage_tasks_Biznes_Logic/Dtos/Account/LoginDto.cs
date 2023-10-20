namespace Manage_tasks_Biznes_Logic.Dtos.Account;

public class LoginDto
{
    public string Email { get; set; }

    public string Password { get; set; }

    public bool RememberMe { get; set; }
}