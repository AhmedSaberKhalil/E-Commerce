using E_CommerceWebApi.Data;

namespace E_CommerceWebApi.Service.AuthenticationService
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);

    }
}
