using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Data
{
    public class DatabaseOperations : IDatabaseOperations
    {
        private readonly AppDatabaseContext context;

        public DatabaseOperations(AppDatabaseContext context)
        {
            this.context = context;
        }

        public Task<int> UpdateDatabase()
        {
            return context.SaveChangesAsync();
        }
    }
}
