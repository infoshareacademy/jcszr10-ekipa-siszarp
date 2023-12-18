namespace Manage_tasks_Biznes_Logic.Dtos.Account;

public class EditEmailResultDto
{
    public bool EditEmailFailed { get; set; }

    public bool NewEmailAlreadyInUse { get; set; }

    public bool NewEmailIsCurrentEmail { get; set; }
}