using AnimalAdoptionCenter.Resources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Profiles
{
    public class UpdatedAnimalToAnimalProfile : Profile
    {
        public UpdatedAnimalToAnimalProfile()
        {
            CreateMap<UpdatedAnimalResource, AnimalAdoptionCenterModels.Animal>();
        }
    }
}
