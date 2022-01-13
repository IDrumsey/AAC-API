using AnimalAdoptionCenter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Repositories.Animal
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AppDatabaseContext _dbContext;

        public AnimalRepository(AppDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AnimalAdoptionCenterModels.Animal> FindAnimalAsync(int id)
        {
            return await _dbContext.Animals.FindAsync(id);
        }

        public AnimalAdoptionCenterModels.Animal FindAnimal(int id)
        {
            return _dbContext.Animals.Include(animal => animal.pictures).SingleOrDefault(animal => animal.AnimalId == id);
        }

        public async Task<List<AnimalAdoptionCenterModels.Animal>> GetAllAnimals()
        {
            return await _dbContext.Animals.ToListAsync();
        }

        public async Task<AnimalAdoptionCenterModels.Store> GetSingleAnimalStoreDetailsAsync(AnimalAdoptionCenterModels.Animal animal)
        {
            return await _dbContext.Stores.FindAsync(animal.storeId);
        }

        public void UpdateContextWithUpdatedAnimalData(AnimalAdoptionCenterModels.Animal updatedAnimal)
        {
            _dbContext.Animals.Update(updatedAnimal);
        }

        public void RemoveAnimal(AnimalAdoptionCenterModels.Animal animalToRemove)
        {
            // this line handles the foreign keys
            // var animal = _dbContext.Animals.Include(animal => animal.pictures).FirstOrDefault();

            // _dbContext.Remove(animal);

            _dbContext.Remove(animalToRemove);
        }

        public async Task<AnimalAdoptionCenterModels.Animal> AddAnimalAsync(AnimalAdoptionCenterModels.Animal animalToAdd)
        {
            await _dbContext.Animals.AddAsync(animalToAdd);
            return animalToAdd;
        }
    }
}
