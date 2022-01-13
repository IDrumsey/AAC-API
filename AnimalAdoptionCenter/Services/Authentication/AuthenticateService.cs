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
using AnimalAdoptionCenter.Services.Users;

namespace AnimalAdoptionCenter.Services.Authentication
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserService _userService;
        public AuthenticateService(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<bool> authenticate(User user)
        {
            // find user in database
            var userFound = await this._userService.GetUserByCredentialsAsync(user);
            if (!userFound.success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
