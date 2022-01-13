using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenterModels;
using AutoMapper;

namespace AnimalAdoptionCenter.Profiles
{
    public class UserToUserResource : Profile
    {
        public UserToUserResource()
        {
            CreateMap<User, UserResource>();
        }
    }
}
