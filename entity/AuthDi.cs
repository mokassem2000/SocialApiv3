using SocialClint._services.Classes;
using SocialClint._services.Interfaces;
using System.Runtime.CompilerServices;

namespace SocialClint.entity
{
    public static class AuthDi
    {
        public static void AddAuthModels(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthService>();
        }
    }
}
