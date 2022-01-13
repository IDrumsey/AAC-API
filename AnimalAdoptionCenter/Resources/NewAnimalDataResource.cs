using AnimalAdoptionCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Resources
{
    public class NewAnimalDataResource
    {
        public NewAnimalResource animal { get; set; }
        public List<SavedFile> images { get; set; }
    }
}
