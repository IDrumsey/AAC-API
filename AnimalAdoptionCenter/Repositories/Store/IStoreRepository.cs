using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Repositories.Store
{
    public interface IStoreRepository
    {
        List<AnimalAdoptionCenterModels.Store> GetAllStores();
        Task<List<AnimalAdoptionCenterModels.Animal>> GetStoreAnimalsAsync(AnimalAdoptionCenterModels.Store store);
        List<AnimalAdoptionCenterModels.DayOperationHours> GetStoreHours(AnimalAdoptionCenterModels.Store store);
        Task<AnimalAdoptionCenterModels.Store> FindSingleStoreAsync(int id);
        AnimalAdoptionCenterModels.Store AddSingleStore(AnimalAdoptionCenterModels.Store storeToAdd);
        AnimalAdoptionCenterModels.Store UpdateStore(AnimalAdoptionCenterModels.Store updatedStore);
        AnimalAdoptionCenterModels.Store DeleteStore(AnimalAdoptionCenterModels.Store storeToDelete);
    }
}
