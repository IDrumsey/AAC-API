using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Services.Communication;
using AnimalAdoptionCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Services.Store
{
    public interface IStoreService
    {
        Response<List<StoreResource>> GetAllStores();
        Task<Response<StoreResource>> GetSingleStoreAsync(int id);
        Task<Response<List<AnimalAdoptionCenterModels.Animal>>> GetStoreAnimalsAsync(int id);
        Task<Response<StoreResource>> AddSingleStoreAsync(NewCenterDataResource newCenter);
        Task<Response<List<DayOperationHoursResource>>> GetStoreHours(int id);
        Task<Response<StoreResource>> UpdateStoreDataAsync(int id, UpdatedStoreResource updatedStoreData);
        Task<Response<StoreResource>> addStorePictures(int storeId, List<SavedFile> files);
        Task<Response<StoreResource>> DeleteStoreAsync(int id);
    }
}
