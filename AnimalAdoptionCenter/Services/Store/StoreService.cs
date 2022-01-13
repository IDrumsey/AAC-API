using AnimalAdoptionCenter.Data;
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

namespace AnimalAdoptionCenter.Services.Store
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly IDatabaseOperations _databaseOperations;
        private readonly FileService _fileService;

        public StoreService(IStoreRepository storeRepository, IMapper mapper, IDatabaseOperations databaseOperations)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
            _databaseOperations = databaseOperations;
            this._fileService = new FileService("wwwroot/Images/Stores");
        }

        public Response<List<StoreResource>> GetAllStores()
        {
            var stores = _storeRepository.GetAllStores();

            var storeResources = _mapper.Map<List<AnimalAdoptionCenterModels.Store>, List<StoreResource>>(stores);

            return new Response<List<StoreResource>>(storeResources);
        }

        public async Task<Response<StoreResource>> GetSingleStoreAsync(int id)
        {
            // find the store
            var storeFound = await _storeRepository.FindSingleStoreAsync(id);

            // check for none found
            if (storeFound == null)
            {
                return new Response<StoreResource>($"Couldn't find a store with id {id}");
            }

            // map to StoreResource
            var storeResource = _mapper.Map<AnimalAdoptionCenterModels.Store, StoreResource>(storeFound);

            // successful response
            return new Response<StoreResource>(storeResource);
        }

        public async Task<Response<List<AnimalAdoptionCenterModels.Animal>>> GetStoreAnimalsAsync(int id)
        {
            // find store
            var storeFound = await _storeRepository.FindSingleStoreAsync(id);

            // check for none found
            if (storeFound == null)
            {
                return new Response<List<AnimalAdoptionCenterModels.Animal>>($"Couldn't find a store with id {id}");
            }

            // get response
            var animals = await _storeRepository.GetStoreAnimalsAsync(storeFound);

            return new Response<List<AnimalAdoptionCenterModels.Animal>>(animals);
        }

        private void saveAndAttachImage(SavedFile file, AnimalAdoptionCenterModels.Store store)
        {
            // 1. attempt to save
            // 2. check if saved
            // 3. create LocationPicture object
            // 3. add LocationPicture object to store

            if (this._fileService.saveFile(file))
            {
                var picture = new LocationPicture();
                picture.path = file.name;
                store.pictures.Add(picture);
            }
        }

        public async Task<Response<StoreResource>> AddSingleStoreAsync(NewCenterDataResource newCenter)
        {
            // map to Store
            var storeToAdd = _mapper.Map<NewStoreResource, AnimalAdoptionCenterModels.Store>(newCenter.store);

            // add any images
            if (newCenter.images != null)
            {
                List<SavedFile> preppedImages = this._fileService.validateAndPrepImages(newCenter.images);

                // for each prepped image
                preppedImages.ForEach(imageFile =>
                {
                    this.saveAndAttachImage(imageFile, storeToAdd);
                });
            }

            // add store to database
            var storeAdded = _storeRepository.AddSingleStore(storeToAdd);

            try
            {
                await _databaseOperations.UpdateDatabase();

                // saved database -> map to StoreResource
                var storeResourceAdded = _mapper.Map<AnimalAdoptionCenterModels.Store, StoreResource>(storeAdded);

                return new Response<StoreResource>(storeResourceAdded);
            }
            catch (Exception e)
            {
                return new Response<StoreResource>($"Couldn't add the store to the database. Error : {e.Message}");
            }
        }

        public async Task<Response<List<DayOperationHoursResource>>> GetStoreHours(int id)
        {
            // find store
            var storeFound = await _storeRepository.FindSingleStoreAsync(id);

            // check for no store found
            if (storeFound == null)
            {
                return new Response<List<DayOperationHoursResource>>($"Couldn't find store with id {id}");
            }

            // find hours
            try
            {
                var hours = _storeRepository.GetStoreHours(storeFound);

                // map to DayOperationHoursResource -> removes the storeId property
                var hoursCondensed = _mapper.Map<List<DayOperationHours>, List<DayOperationHoursResource>>(hours);

                return new Response<List<DayOperationHoursResource>>(hoursCondensed);
            }
            catch (Exception e)
            {
                return new Response<List<DayOperationHoursResource>>($"Couldn't get store {id}'s operation hours. Error : {e.Message}");
            }
        }

        public async Task<Response<StoreResource>> UpdateStoreDataAsync(int id, UpdatedStoreResource updatedStoreData)
        {
            // find the store to update
            var storeFound = await _storeRepository.FindSingleStoreAsync(id);

            // check for no store found
            if (storeFound == null)
            {
                return new Response<StoreResource>($"Couldn't find a store with the id {id}");
            }

            // found store -> map to Store
            var updateData = _mapper.Map<UpdatedStoreResource, AnimalAdoptionCenterModels.Store>(updatedStoreData);

            // update the store's data
            storeFound.address = updateData.address;
            storeFound.pictures.Concat(updatedStoreData.pictures);
            storeFound.operationHours = updatedStoreData.operationHours;

            try
            {
                // update the database
                await _databaseOperations.UpdateDatabase();

                // map to StoreResource
                var updatedStoreCondensed = _mapper.Map<AnimalAdoptionCenterModels.Store, StoreResource>(storeFound);

                return new Response<StoreResource>(updatedStoreCondensed);
            }
            catch (Exception e)
            {
                return new Response<StoreResource>($"Couldn't update the store with the id {id}. Error : {e.Message}");
            }
        }

        public async Task<Response<StoreResource>> addStorePictures(int storeId, List<SavedFile> files)
        {
            // find store
            var storeFound = await this._storeRepository.FindSingleStoreAsync(storeId);

            // check if store was found
            if (storeFound == null)
            {
                // no store found
                return new Response<StoreResource>($"Couldn't find the store with id {storeId} to add the images to.");
            }

            // prep pictures
            var preppedFiles = this._fileService.validateAndPrepImages(files);

            // save pictures to server
            preppedFiles.ForEach(file =>
            {
                if (this._fileService.saveFile(file))
                {
                    // convert to LocationPicture object
                    var locationPicture = new LocationPicture();
                    locationPicture.path = file.name;
                    storeFound.pictures.Add(locationPicture);
                }
            });

            // update database
            await this._databaseOperations.UpdateDatabase();

            // map store to StoreResource
            var resource = this._mapper.Map<AnimalAdoptionCenterModels.Store, StoreResource>(storeFound);

            // return result
            return new Response<StoreResource>(resource);
        }

        public async Task<Response<StoreResource>> DeleteStoreAsync(int id)
        {
            // find the store
            var storeFound = await _storeRepository.FindSingleStoreAsync(id);

            // check if no store was found
            if (storeFound == null)
            {
                return new Response<StoreResource>($"Couldn't find a store with the id {id}");
            }

            // found store -> delete the store
            _storeRepository.DeleteStore(storeFound);

            // update the database
            try
            {
                await _databaseOperations.UpdateDatabase();

                // map to StoreResource
                var deletedStoreCondensed = _mapper.Map<AnimalAdoptionCenterModels.Store, StoreResource>(storeFound);

                return new Response<StoreResource>(deletedStoreCondensed);
            }
            catch (Exception e)
            {
                return new Response<StoreResource>($"Couldn't update the database. The store wasn't deleted. Error : {e.Message}");
            }
        }
    }
}
