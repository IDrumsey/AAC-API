using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Data
{
    public interface IDatabaseOperations
    {
        Task<int> UpdateDatabase();
    }
}
