using AnimalAdoptionCenter.Extensions;
using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Services.Animal;
using AnimalAdoptionCenter.Services.Authentication;
using AnimalAdoptionCenterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Controllers
{
    [Route("/api/[controller]")]
    [Authorize]
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly ITokenService _tokenService;

        public AnimalsController(IAnimalService animalService, ITokenService tokenService)
        {
            _animalService = animalService;
            this._tokenService = tokenService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetSingleAnimalDetails(int id)
        {
            // call store getall service
            var response = _animalService.SingleAnimalDetails(id);

            return Ok(response.data);

            //// check the response
            //if (!response.success)
            //{
            //    return BadRequest(response.message);
            //}

            //// successful response
            //return Ok(response.data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAnimalDetails()
        {
            var response = await _animalService.GetAllAnimalsAsync();

            if (!response.success)
            {
                return BadRequest(response.message);
            }

            return Ok(response.data);
        }

        [HttpGet("{id}/store")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleAnimalsStore(int id)
        {
            var response = await _animalService.GetSingleAnimalStoreDetailsAsync(id);

            if (!response.success)
            {
                return BadRequest(response.message);
            }

            return Ok(response.data);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddAnimal([FromBody] NewAnimalResource newAnimalData)
        //{
        //    // validate data
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState.GetAllErrorMessages());
        //    }

        //    // valid data -> get response
        //    var response = await _animalService.AddNewAnimalAsync(newAnimalData);

        //    if (!response.success)
        //    {
        //        return BadRequest(response.message);
        //    }

        //    return Ok(response.data);
        //}

        [HttpPost]
        public async Task<IActionResult> NewAnimal([FromBody] NewAnimalDataResource data)
        {
            // create the new animal
            var res = await _animalService.AddNewAnimalAsync(data);

            // check if the animal was added properly
            if (!res.success)
            {
                return BadRequest(res.message);
            }

            return Ok(res.data);
        }


        private List<string> AddAnimalPictures(int id, List<SavedFile> files)
        {
            List<string> returnMessages = new List<string>();

            // https://www.codemag.com/Article/1901061/Upload-Small-Files-to-a-Web-API-Using-Angular

            files.ForEach(file =>
            {
                // for each file check if it already exists
                bool exists = _animalService.doesFileExist(file.name);
                if (!exists)
                {
                    // might have extra information about the file that we don't need?
                    if (file.asBase64.Contains(','))
                    {
                        // remove the metadata from the base64 data
                        string cleanedBase64 = file.asBase64.Substring(file.asBase64.IndexOf(',') + 1);
                        // convert the base64 to binary format
                        file.asByteArray = Convert.FromBase64String(cleanedBase64);
                        // save the file
                        string fullPath = "wwwroot/Images/Animals/" + file.name;
                        // using deletes the filestream after block ends I think
                        using (var fs = new FileStream(fullPath, FileMode.CreateNew))
                        {
                            try
                            {
                                fs.Write(file.asByteArray, 0, file.asByteArray.Length);

                                // good write
                                returnMessages.Add(file.name);
                            }
                            catch (Exception)
                            {
                                //returnMessages.Add($"{file.name} - Error: {e.Message}");
                                throw;
                            }
                        }
                    }
                }
                else
                {
                    // file already exists -> still want to return it in the list of added pictures so it's connected to the animal
                    returnMessages.Add(file.name);
                }
            });

            return returnMessages;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimalData(int id, [FromBody] UpdatedAnimalResource data)
        {
            // validate data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetAllErrorMessages());
            }

            // valid data -> get response
            var response = await _animalService.UpdateAnimalAsync(id, data);

            if (!response.success)
            {
                return BadRequest(response.message);
            }

            return Ok(response.data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAnimalById(int id)
        {
            // get response
            var response = await _animalService.RemoveAnimalAsync(id);

            if (!response.success)
            {
                return BadRequest(response.message);
            }

            return Ok(response.data);
        }
    }
}
