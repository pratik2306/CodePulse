using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.Repositories.Abstract
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
