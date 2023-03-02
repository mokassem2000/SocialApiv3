using SocialClint._models;

namespace SocialClint._services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> Register(RegisterModel model);

        Task<AuthModel> GetToken(TokenRequest model);

    }
}
