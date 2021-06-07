using Microsoft.IdentityModel.Tokens;
using Rentering.Accounts.Domain.Entities;
using Rentering.WebAPI.Authorization.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rentering.WebAPI.Authorization.Services
{
    public static class TokenService
    {
        public static UserInfoModel GenerateToken(AccountEntity account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Id.ToString()),
                    new Claim(ClaimTypes.Role, account.Role.ToString())
                }),

                Expires = DateTime.Now.AddHours(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userInfoModel = new UserInfoModel(account.Id, account.Username.ToString(), tokenHandler.WriteToken(token), account.Role);

            return userInfoModel;
        }
    }
}
