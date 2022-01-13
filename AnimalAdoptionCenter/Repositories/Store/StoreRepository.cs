using AnimalAdoptionCenter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Repositories.Store
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDatabaseContext _dbContext;

        public StoreRepository(AppDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AnimalAdoptionCenterModels.Store> GetAllStores()
        {
            return _dbContext.Stores.Include(store => store.pictures).ToList();
        }

        public async Task<AnimalAdoptionCenterModels.Store> FindSingleStoreAsync(int id)
        {
            return await _dbContext.Stores.Include(store => store.pictures).Include(store => store.operationHours).FirstOrDefaultAsync(store => store.StoreId == id);
        }

        public async Task<List<AnimalAdoptionCenterModels.Animal>> GetStoreAnimalsAsync(AnimalAdoptionCenterModels.Store store)
        {
            return await _dbContext.Animals.Include(animal => animal.pictures).Where(animal => animal.storeId == store.StoreId).ToListAsync();
        }

        public List<AnimalAdoptionCenterModels.DayOperationHours> GetStoreHours(AnimalAdoptionCenterModels.Store store)
        {
            return _dbContext.Stores.Include(store => store.operationHours).ThenInclude(hours => hours.times).SingleOrDefault(s => s.StoreId == store.StoreId).operationHours.ToList();
        }

        public AnimalAdoptionCenterModels.Store AddSingleStore(AnimalAdoptionCenterModels.Store storeToAdd)
        {
            _dbContext.Stores.Add(storeToAdd);

            return storeToAdd;
        }

        public AnimalAdoptionCenterModels.Store UpdateStore(AnimalAdoptionCenterModels.Store updatedStore)
        {
            _dbContext.Stores.Update(updatedStore);
            return updatedStore;
        }

        public AnimalAdoptionCenterModels.Store DeleteStore(AnimalAdoptionCenterModels.Store storeToDelete)
        {
            _dbContext.Stores.Remove(storeToDelete);
            return storeToDelete;
        }
    }
}
