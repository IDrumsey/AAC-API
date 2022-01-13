using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAdoptionCenterModels;

namespace AnimalAdoptionCenter.Repositories.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> FindUserById(int id);
        Task<User> FindUserByUsernameAndPassword(string username, string password);
    }
}
