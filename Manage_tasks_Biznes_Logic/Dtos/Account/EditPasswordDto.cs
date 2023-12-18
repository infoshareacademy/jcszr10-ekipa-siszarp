namespace Manage_tasks_Biznes_Logic.Dtos.Account;

public class EditPasswordDto
{
    public Guid UserId { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }

    public string ConfirmNewPassword { get; set; }
}