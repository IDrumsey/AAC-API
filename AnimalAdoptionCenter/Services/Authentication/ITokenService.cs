using AnimalAdoptionCenter.Resources;
using AnimalAdoptionCenter.Services.Communication;
using AnimalAdoptionCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAdoptionCenter.Services.Authentication
{
    public interface ITokenService
    {
        string createToken(UserResource credentials);
    }
}
