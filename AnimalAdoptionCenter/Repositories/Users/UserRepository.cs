using AnimalAdoptionCenter.Data;
using AnimalAdoptionCenterModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDatabaseContext _dbContext;
        public UserRepository(AppDatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public Task<List<User>> GetAllUsers()
        {
            return this._dbContext.Users.ToListAsync();
        }

        public async Task<User> FindUserById(int id)
        {
            return await this._dbContext.Users.FindAsync(id);
        }

        public async Task<User> FindUserByUsernameAndPassword(string username, string password)
        {
            return await this._dbContext.Users.FirstOrDefaultAsync(u => u.username == username && u.password == password);
        }
    }
}
