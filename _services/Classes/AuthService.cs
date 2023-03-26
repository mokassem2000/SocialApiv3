
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialClint._models;
using SocialClint._services.Interfaces;
using SocialClint.Dto;
using SocialClint.entity;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Text;

namespace SocialClint._services.Classes
{
    public class AuthService : IAuthService
    {
        public UserManager<AppUser> _userManager { get; }
        public Jwt _Jwt { get; }

        public AuthService(UserManager<AppUser> userManager, IOptions<Jwt> jwt)
        {
            _Jwt = jwt.Value;
       
            _userManager = userManager;

        }
        public async Task<AuthModel> Register(RegisterModel model)
        {
           
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return new AuthModel() { Message = "there is a user alredy with this email" };
            }
            if (await _userManager.FindByNameAsync(model.Username) != null)
            {
                return new AuthModel { Message = "Username is already registered!" };
            }

            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Username,
            };
            
            var rslt = await _userManager.CreateAsync(user, model.Password);
            if (!rslt.Succeeded)
            {

                var err = string.Empty;
                foreach (var item in rslt.Errors)
                {
                    err += item;
                }
                return new AuthModel() { Message = err };

            }
            var token = await CreateJwtToken(user);

            return new AuthModel() { Id=user.Id,Message = "ok",Name=user.UserName, IsAuthenticated = true, Token=new JwtSecurityTokenHandler().WriteToken(token) };

        }
        public async  Task<AuthModel> GetToken(TokenRequest model)
        {

            var authModel = new AuthModel();

            var user = await   _userManager.FindByEmailAsync(model.Email);

            if (user is null || ! await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }


            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.Id=user.Id;
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Name= user.UserName;

            return authModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claimes = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id) }.Union(userClaims);
                



            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _Jwt.Issuer,
                audience: _Jwt.Audience,
                claims: claimes,
                expires: DateTime.Now.AddDays(_Jwt.DurationInDays),
                signingCredentials: signingCredentials); ;
            return token;

        }


    }
}
