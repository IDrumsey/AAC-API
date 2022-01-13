using AnimalAdoptionCenter.Resources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Profiles
{
    public class NewAnimalToAnimalProfile : Profile
    {
        public NewAnimalToAnimalProfile()
        {
            CreateMap<NewAnimalResource, AnimalAdoptionCenterModels.Animal>();
        }
    }
}
