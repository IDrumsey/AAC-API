using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenterModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Profiles
{
    public class SimpleTimeToSimpleTimeResourceProfile : Profile
    {
        public SimpleTimeToSimpleTimeResourceProfile()
        {
            CreateMap<SimpleTime, SimpleTimeResource>();
        }
    }
}
