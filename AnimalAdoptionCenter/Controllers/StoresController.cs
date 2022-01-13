using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAdoptionCenterModels;
using AnimalAdoptionCenter.Services.Store;
using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AnimalAdoptionCenter.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]
    public class StoresController : Controller
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            // call store getall service
            var response = _storeService.GetAllStores();

            // check the response
            if (!response.success)
            {
                return BadRequest(response.message);
            }

            // successful response
            return Ok(response.data);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStoreById(int id)
        {
            // get response
            var response = await _storeService.GetSingleStoreAsync(id);

            // check the response
            if (!response.success)
            {
                return BadRequest(response.message);
            }

            // successful response
            return Ok(response.data);
        }

        [HttpGet("{id}/animals")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStoreAnimals(int id)
        {
            // get response
            var response = await _storeService.GetStoreAnimalsAsync(id);

            // check for error
            if (!response.success)
            {
                return BadRequest(response.message);
            }

            // successful response
            return Ok(response.data);
        }

        [HttpGet("{id}/hours")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleStoreHours(int id)
        {
            // get response
            var response = await _storeService.GetStoreHours(id);

            // check for error
            if (!response.success)
            {
                return BadRequest(response.message);
            }

            // successful response
            return Ok(response.data);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStore([FromBody] NewCenterDataResource storeData)
        {
            if (!ModelState.IsValid)
            {
                // get error msgs
                var errors = ModelState.GetAllErrorMessages();
                return BadRequest(errors);
            }
            // get response
            var response = await _storeService.AddSingleStoreAsync(storeData);

            // check for error
            if (!response.success)
            {
                return BadRequest(response.message);
            }

            // successful response
            return Ok(response.data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] UpdatedStoreResource updatedStoreData)
        {
            // validate the data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetAllErrorMessages());
            }

            // get response
            var response = await _storeService.UpdateStoreDataAsync(id, updatedStoreData);

            // check for error
            if (!response.success)
            {
                return BadRequest(response.message);
            }

            // successful response
            return Ok(response.data);
        }

        [HttpPut("{id}/pictures")]
        public async Task<IActionResult> AddStorePictures(int id, [FromBody] List<SavedFile> imageFiles)
        {
            // call service and wait for response
            var res = await this._storeService.addStorePictures(id, imageFiles);
            // check if service succeeded
            if (!res.success)
            {
                return BadRequest(res.message);
            }

            return Ok(res.data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreAsync(int id)
        {
            // get response
            var response = await _storeService.DeleteStoreAsync(id);

            // check for error
            if (!response.success)
            {
                return BadRequest(response.message);
            }

            // successful response
            return Ok(response.data);
        }
    }
}
