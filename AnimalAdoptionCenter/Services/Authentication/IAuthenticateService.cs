using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Services.Communication;
using AnimalAdoptionCenterModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Services.Authentication
{
    public interface IAuthenticateService
    {
        Task<bool> authenticate(User user);
    }
}
