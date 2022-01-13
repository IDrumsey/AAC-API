using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Services.Communication;
using AnimalAdoptionCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Services.Users
{
    public interface IUserService
    {
        Task<Response<List<User>>> GetAllUsersAsync();
        Task<Response<UserResource>> GetUserByCredentialsAsync(User user);
    }
}
