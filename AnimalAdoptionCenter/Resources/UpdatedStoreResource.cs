using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AnimalAdoptionCenterModels;

namespace AnimalAdoptionCenter.Resources
{
    public class UpdatedStoreResource
    {
        public string address { get; set; }
        public List<DayOperationHours> operationHours { get; set; }
        public List<LocationPicture> pictures { get; set; }
    }
}
