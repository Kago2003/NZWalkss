using Microsoft.AspNetCore.Identity;

namespace NZWalkssAPI.Repositories
{
    public interface ITokenRepository
    {
        String CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
