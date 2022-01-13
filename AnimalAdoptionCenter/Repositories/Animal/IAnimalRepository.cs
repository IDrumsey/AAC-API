using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Repositories.Animal
{
    public interface IAnimalRepository
    {
        Task<List<AnimalAdoptionCenterModels.Animal>> GetAllAnimals();
        Task<AnimalAdoptionCenterModels.Animal> FindAnimalAsync(int id);
        Task<AnimalAdoptionCenterModels.Store> GetSingleAnimalStoreDetailsAsync(AnimalAdoptionCenterModels.Animal animal);
        void UpdateContextWithUpdatedAnimalData(AnimalAdoptionCenterModels.Animal updatedAnimal);
        void RemoveAnimal(AnimalAdoptionCenterModels.Animal animalToRemove);
        Task<AnimalAdoptionCenterModels.Animal> AddAnimalAsync(AnimalAdoptionCenterModels.Animal animalToAdd);
        AnimalAdoptionCenterModels.Animal FindAnimal(int id);
    }
}
