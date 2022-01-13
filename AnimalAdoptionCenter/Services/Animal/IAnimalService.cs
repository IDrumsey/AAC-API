using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Services.Communication;
using AnimalAdoptionCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Services.Animal
{
    public interface IAnimalService
    {
        Task<Response<AnimalAdoptionCenterModels.Animal>> SingleAnimalDetailsAsync(int id);
        Response<AnimalAdoptionCenterModels.Animal> SingleAnimalDetails(int id);
        Task<Response<List<AnimalAdoptionCenterModels.Animal>>> GetAllAnimalsAsync();
        Task<Response<StoreResource>> GetSingleAnimalStoreDetailsAsync(int animalId);
        Task<Response<AnimalAdoptionCenterModels.Animal>> UpdateAnimalAsync(int id, UpdatedAnimalResource updatedData);
        Task<Response<AnimalAdoptionCenterModels.Animal>> RemoveAnimalAsync(int id);
        Task<Response<AnimalAdoptionCenterModels.Animal>> AddNewAnimalAsync(NewAnimalDataResource newAnimal);
        string getUniqueFileName(SavedFile f);
        bool doesFileExist(string fileName);
        Task<Response<AnimalAdoptionCenterModels.Animal>> AddAnimalPicturesAsync(int animalId, List<string> fileNames);
    }
}
