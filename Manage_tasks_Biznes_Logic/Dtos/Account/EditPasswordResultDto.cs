namespace Manage_tasks_Biznes_Logic.Dtos.Account;

public class EditPasswordResultDto
{
    public bool EditPasswordFailed { get; set; }

    public bool WrongCurrentPassword { get; set; }

    public bool NewPasswordIsOldPassword { get; set; }

    public bool PasswordsAreEqual { get; set; }
}