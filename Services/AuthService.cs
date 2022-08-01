using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Forum._3.Services.Abstraction;
using Forum._3.Models.ViewModels;
using Forum._3.Models;
using CryptoHelper;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Forum._3.Services
{ 
    public class AuthService: IAuthService
    {

        public AuthData GetAuthData(User user)
        {
            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);


            var expirationTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));


            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    //issuer: AuthOptions.ISSUER,
                    //audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: claimsIdentity.Claims,
                    expires: expirationTime,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256Signature));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);




            return new AuthData{
                Token = encodedJwt,
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                Id = user.Id,
                Name = user.Username,
                IsAdmin = user.Role.Name=="admin"
            };
        }
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public bool VerifyPassword(string actualPassword, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, actualPassword);
        }

        




    }
}
