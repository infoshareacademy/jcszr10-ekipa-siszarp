using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Manage_tasks_Biznes_Logic.Dtos.Account;

public class LoginResultDto
{
    [MemberNotNullWhen(true, nameof(UserId), nameof(AuthProp), nameof(ClaimsIdentity))]
    public bool LoginWasSuccessful { get; set; }

    public Guid? UserId { get; set; }

    public AuthenticationProperties? AuthProp { get; set; }

    public ClaimsIdentity? ClaimsIdentity { get; set; }
}