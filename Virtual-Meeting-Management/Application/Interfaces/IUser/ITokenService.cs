using System.Security.Claims;

namespace Application.Interfaces.IUser
{
    public interface ITokenService
    {
        string GenerateToken(IList<Claim> claims);
    }
}