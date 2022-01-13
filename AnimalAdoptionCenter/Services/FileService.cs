using System;
using System.Collections.Generic;
using System.IO;
using AnimalAdoptionCenterModels;

namespace AnimalAdoptionCenter.Services
{
    public class FileService
    {
        private string directory;

        public FileService(string directory)
        {
            this.directory = directory;
        }

        // https://www.codemag.com/Article/1901061/Upload-Small-Files-to-a-Web-API-Using-Angular

        public bool doesFileExist(SavedFile file)
        {
            // build full path
            string fullPath = $"{this.directory}/{file.name}";
            return File.Exists(fullPath);
        }

        public string getPixelDataFromBase64String(string base64ImageString)
        {
            return base64ImageString.Substring(base64ImageString.IndexOf(',') + 1);
        }

        public string getUniqueFileName(SavedFile f)
        {
            int lastPeriod = f.name.LastIndexOf('.');
            //split
            string start = f.name.Substring(0, lastPeriod);
            string extension = f.name.Substring(lastPeriod);

            return $"{start}-{f.lastModified}{extension}";
        }

        public List<SavedFile> validateAndPrepImages(List<SavedFile> imageFiles)
        {
            // for each image file -
            // 1. check if exists already
            // 2. update name to be unique
            // 3. remove meta data from the base64 string
            // 4. generate byte array from base64 string
            // 5. add image to list to return

            List<SavedFile> validatedAndPreppedImages = new List<SavedFile>();

            imageFiles.ForEach(file =>
            {
                if (!this.doesFileExist(file))
                {
                    // file doesn't exist already
                    file.name = this.getUniqueFileName(file);
                    file.asBase64 = this.getPixelDataFromBase64String(file.asBase64);
                    file.asByteArray = Convert.FromBase64String(file.asBase64);
                    validatedAndPreppedImages.Add(file);
                }
            });

            return validatedAndPreppedImages;
        }

        public bool saveFile(SavedFile file)
        {
            var path = $"{this.directory}/{file.name}";
            using (var fs = new FileStream(path, FileMode.Create))
            {
                try
                {
                    fs.Write(file.asByteArray, 0, file.asByteArray.Length);
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }
    }
}