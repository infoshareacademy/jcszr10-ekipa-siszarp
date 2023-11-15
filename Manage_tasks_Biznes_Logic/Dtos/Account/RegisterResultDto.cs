namespace Manage_tasks_Biznes_Logic.Dtos.Account;

public class RegisterResultDto
{
    public bool RegistrationFailed { get; set; }

    public bool PasswordsAreEqual { get; set; }

    public bool EmailAlreadyInUse { get; set; }
}