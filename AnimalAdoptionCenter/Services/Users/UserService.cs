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
using AnimalAdoptionCenter.Repositories.Users;
using AnimalAdoptionCenter.Services.Authentication;

namespace AnimalAdoptionCenter.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository usersRepo, IPasswordHasher passwordHasher, IMapper mapper)
        {
            this._usersRepo = usersRepo;
            this._passwordHasher = passwordHasher;
            this._mapper = mapper;
        }

        public async Task<Response<List<User>>> GetAllUsersAsync()
        {
            var users = await this._usersRepo.GetAllUsers();
            return new Response<List<User>>(users);
        }

        public async Task<Response<User>> GetUserById(int id)
        {
            var userFound = await this._usersRepo.FindUserById(id);

            // check for no user found
            if (userFound == null)
            {
                return new Response<User>($"Couldn't find the user with id {id}");
            }

            // found user
            return new Response<User>(userFound);
        }

        public async Task<Response<UserResource>> GetUserByCredentialsAsync(User user)
        {
            // hash the provided plaintext password
            var hashedPassword = this._passwordHasher.HashPassword(user.password);

            var userFound = await this._usersRepo.FindUserByUsernameAndPassword(user.username, hashedPassword);

            // check for no user found
            if (userFound == null)
            {
                return new Response<UserResource>($"Couldn't find a user with the specified credentials");
            }

            // map to a UserResource to remove password from response
            var userResource = this._mapper.Map<User, UserResource>(userFound);

            // found user
            return new Response<UserResource>(userResource);
        }
    }
}
