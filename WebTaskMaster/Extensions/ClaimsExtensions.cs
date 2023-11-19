using System.Security.Claims;

namespace WebTaskMaster.Extensions;

public static class ClaimsExtensions
{
    public static bool TryGetAuthenticatedUserId(this IEnumerable<Claim> claims, out Guid userId)
    {
        var userIdText = claims
            .Where(c => c.Type == "UserId")
            .Select(c => c.Value)
            .FirstOrDefault();

        return Guid.TryParse(userIdText, out userId);
    }
}