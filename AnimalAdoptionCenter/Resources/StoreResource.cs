using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAdoptionCenterModels;

namespace AnimalAdoptionCenter.Resources
{
    public class StoreResource
    {
        public int StoreId { get; set; }
        public string address { get; set; }
        public List<LocationPicture> pictures { get; set; }
    }
}
