using AnimalAdoptionCenter.Data;
using AnimalAdoptionCenter.Repositories.Animal;
using AnimalAdoptionCenter.Repositories.Store;
using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Services.Communication;
using AnimalAdoptionCenterModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AnimalAdoptionCenter.Services.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            this._config = config;
        }

        public string createToken(UserResource credentials)
        {
            // resources I used for setting up authentication and authorization
            // https://www.youtube.com/watch?v=Lh82WlOvyQk
            // https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._config["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Adding roles to the users
                // https://jasonwatmore.com/post/2019/10/16/aspnet-core-3-role-based-authorization-tutorial-with-example-api#user-cs
                Subject = new ClaimsIdentity(new Claim[] { new Claim("userId", credentials.UserId.ToString()), new Claim(ClaimTypes.Name, credentials.username), new Claim(ClaimTypes.Role, credentials.role) }),
                Expires = DateTime.UtcNow.AddHours(1), //expire time - expire time defined in two places, might need to fix
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
