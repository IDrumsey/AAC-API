using AnimalAdoptionCenter.Data;
using AnimalAdoptionCenter.Repositories.Animal;
using AnimalAdoptionCenter.Repositories.Store;
using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Services.Communication;
using AnimalAdoptionCenterModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Services.Animal
{
    public class AnimalService : IAnimalService
    {
        public const string imagesPath = "wwwroot/Images/Animals/";

        private readonly IAnimalRepository _animalRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly IDatabaseOperations _databaseOperations;
        private readonly FileService _fileService;

        public AnimalService(IAnimalRepository animalRepository, IStoreRepository storeRepository, IMapper mapper, IDatabaseOperations databaseOperations)
        {
            this._animalRepository = animalRepository;
            this._storeRepository = storeRepository;
            this._mapper = mapper;
            this._databaseOperations = databaseOperations;
            this._fileService = new FileService("wwwroot/Images/Animals");
        }

        public async Task<Response<AnimalAdoptionCenterModels.Animal>> SingleAnimalDetailsAsync(int id)
        {
            // find the animal
            var animalFound = await _animalRepository.FindAnimalAsync(id);

            // check for no animal found
            if (animalFound == null)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>($"Couldn't find an animal with the id {id}");
            }

            // map to AnimalResource
            return new Response<AnimalAdoptionCenterModels.Animal>(animalFound);
        }

        public Response<AnimalAdoptionCenterModels.Animal> SingleAnimalDetails(int id)
        {
            // find the animal
            var animalFound = _animalRepository.FindAnimal(id);

            // check for no animal found
            if (animalFound == null)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>($"Couldn't find an animal with the id {id}");
            }

            // map to AnimalResource
            return new Response<AnimalAdoptionCenterModels.Animal>(animalFound);
        }

        public async Task<Response<List<AnimalAdoptionCenterModels.Animal>>> GetAllAnimalsAsync()
        {
            try
            {
                var animals = await _animalRepository.GetAllAnimals();
                return new Response<List<AnimalAdoptionCenterModels.Animal>>(animals);
            }
            catch (Exception e)
            {
                return new Response<List<AnimalAdoptionCenterModels.Animal>>($"Couldn't get all of the animals' details. Error : {e.Message}");
            }
        }

        public async Task<Response<StoreResource>> GetSingleAnimalStoreDetailsAsync(int animalId)
        {
            // find the animal
            var animalFound = await _animalRepository.FindAnimalAsync(animalId);

            // check if no animal was found
            if (animalFound == null)
            {
                return new Response<StoreResource>($"Couldn't find an animal with the id {animalId}");
            }

            // get the stores details
            var storeDetails = await _animalRepository.GetSingleAnimalStoreDetailsAsync(animalFound);

            // map to StoreResource
            var storeCondensed = _mapper.Map<AnimalAdoptionCenterModels.Store, StoreResource>(storeDetails);

            return new Response<StoreResource>(storeCondensed);
        }

        public async Task<Response<AnimalAdoptionCenterModels.Animal>> UpdateAnimalAsync(int id, UpdatedAnimalResource updatedData)
        {
            // check for defined empty strings that aren't supposed to be empty
            var emptyStringErrorMessages = updatedData.GetEmptyStringErrors();

            if (emptyStringErrorMessages != null)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>(emptyStringErrorMessages);
            }

            // find the animal
            var animalFound = await _animalRepository.FindAnimalAsync(id);

            // check for no animal found
            if (animalFound == null)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>($"Couldn't find an animal with the id {id}");
            }

            // map to Animal
            var animalUpdateData = _mapper.Map<UpdatedAnimalResource, AnimalAdoptionCenterModels.Animal>(updatedData);

            // update values
            if (animalUpdateData.name != default(string) && animalUpdateData.name != "")
            {
                animalFound.name = animalUpdateData.name;
            }
            if (animalUpdateData.age != default(int))
            {
                animalFound.age = animalUpdateData.age;
            }
            if (animalUpdateData.gender != default(char))
            {
                animalFound.gender = animalUpdateData.gender;
            }
            if (animalUpdateData.classificationName != default(string) && animalUpdateData.classificationName != "")
            {
                animalFound.classificationName = animalUpdateData.classificationName;
            }
            if (animalUpdateData.species != default(string) && animalUpdateData.species != "")
            {
                animalFound.species = animalUpdateData.species;
            }
            if (animalUpdateData.heightInches != default(int))
            {
                animalFound.heightInches = animalUpdateData.heightInches;
            }
            if (animalUpdateData.weight != default(int))
            {
                animalFound.weight = animalUpdateData.weight;
            }
            if (animalUpdateData.favoriteToy != default(string) && animalUpdateData.favoriteToy != "")
            {
                animalFound.favoriteToy = animalUpdateData.favoriteToy;
            }
            if (animalUpdateData.favoriteActivity != default(string) && animalUpdateData.favoriteActivity != "")
            {
                animalFound.favoriteActivity = animalUpdateData.favoriteActivity;
            }
            if (animalUpdateData.description != default(string))
            {
                animalFound.description = animalUpdateData.description;
            }
            if (animalUpdateData.storeId != default(int))
            {
                // find the store
                var storeFound = await _storeRepository.FindSingleStoreAsync(animalUpdateData.storeId);

                // check for no store found
                if (storeFound == null)
                {
                    return new Response<AnimalAdoptionCenterModels.Animal>($"Data not updated. Couldn't find a store with the id {updatedData.storeId}");
                }
                animalFound.storeId = animalUpdateData.storeId;
            }

            // save database
            try
            {
                await _databaseOperations.UpdateDatabase();

                return new Response<AnimalAdoptionCenterModels.Animal>(animalFound);
            }
            catch (Exception e)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>($"Couldn't update {animalFound.name}'s information. Error : {e.Message}");
            }
        }

        public async Task<Response<AnimalAdoptionCenterModels.Animal>> RemoveAnimalAsync(int id)
        {
            // find animal
            var animalFound = await _animalRepository.FindAnimalAsync(id);

            // check for no animal found
            if (animalFound == null)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>($"Couldn't find an animal with the id {id}");
            }

            try
            {
                // found animal -> remove animal
                _animalRepository.RemoveAnimal(animalFound);

                // update the database
                await _databaseOperations.UpdateDatabase();
            }
            catch (Exception e)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>($"Couldn't update the database. The animal was not removed. Error : {e.Message}");
            }

            return new Response<AnimalAdoptionCenterModels.Animal>(animalFound);
        }

        private void saveAndAttachImage(SavedFile file, AnimalAdoptionCenterModels.Animal animal)
        {
            // 1. attempt to save
            // 2. check if saved
            // 3. create LocationPicture object
            // 3. add LocationPicture object to store

            if (this._fileService.saveFile(file))
            {
                var picture = new FileName();
                picture.path = file.name;
                animal.pictures.Add(picture);
            }
        }

        public async Task<Response<AnimalAdoptionCenterModels.Animal>> AddNewAnimalAsync(NewAnimalDataResource newAnimal)
        {
            // check for defined empty strings that aren't supposed to be empty
            var emptyStringErrorMessages = newAnimal.animal.GetEmptyStringErrors();

            if (emptyStringErrorMessages != null)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>(emptyStringErrorMessages);
            }

            // check if storeId was set
            if (newAnimal.animal.storeId == default(int))
            {
                return new Response<AnimalAdoptionCenterModels.Animal>("StoreId needs to be provided and must be a positive number.");
            }

            // check if store exists
            var storeFound = await _storeRepository.FindSingleStoreAsync(newAnimal.animal.storeId);

            if (storeFound == null)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>($"Couldn't find a store with the id {newAnimal.animal.storeId}");
            }

            // map to Animal
            var animal = _mapper.Map<NewAnimalResource, AnimalAdoptionCenterModels.Animal>(newAnimal.animal);

            // add any images
            List<SavedFile> preppedImageFiles = this._fileService.validateAndPrepImages(newAnimal.images);

            preppedImageFiles.ForEach(imageFile =>
            {
                this.saveAndAttachImage(imageFile, animal);
            });

            try
            {
                // add to context
                var savedAnimal = await _animalRepository.AddAnimalAsync(animal);

                // update database
                await _databaseOperations.UpdateDatabase();

                return new Response<AnimalAdoptionCenterModels.Animal>(savedAnimal);
            }
            catch (Exception e)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>($"Couldn't update the database. The animal was not added. Error : {e.Message}");
            }
        }

        public bool doesFileExist(string fileName)
        {
            string fullPath = $"{imagesPath}{fileName}";
            return File.Exists(fullPath);
        }

        public string getUniqueFileName(SavedFile f)
        {
            int lastPeriod = f.name.LastIndexOf('.');
            //split
            string start = f.name.Substring(0, lastPeriod);
            string extension = f.name.Substring(lastPeriod);

            return $"{start}-{f.lastModified}{extension}";
        }

        public async Task<Response<AnimalAdoptionCenterModels.Animal>> AddAnimalPicturesAsync(int animalId, List<string> fileNames)
        {
            // find animal
            var animalFound = await _animalRepository.FindAnimalAsync(animalId);

            // check if no animal was found
            if (animalFound == null)
            {
                return new Response<AnimalAdoptionCenterModels.Animal>("Couldn't find the animal to add the pictures to.");
            }

            var animalPictures = new List<FileName>();

            // found animal -> add the pictures
            fileNames.ForEach(path =>
            {
                // create a new FileName obj
                var fileName = new FileName();
                fileName.path = path;
                animalPictures.Add(fileName);
            });

            // add the fileNames to the Animal
            if (animalFound.pictures == null)
            {
                animalFound.pictures = new List<FileName>(animalPictures);

            }
            else
            {
                // add onto the already existing list of pictures
                animalFound.pictures.Concat(animalPictures);
            }

            // update the context and save the database
            _animalRepository.UpdateContextWithUpdatedAnimalData(animalFound);
            await _databaseOperations.UpdateDatabase(); // might want to check if the database saved properly

            return new Response<AnimalAdoptionCenterModels.Animal>(animalFound);
        }
    }
}
