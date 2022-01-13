using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AnimalAdoptionCenterModels;

namespace AnimalAdoptionCenter.Resources
{
    public class NewStoreResource
    {
        [Required]
        public string address { get; set; }
        [Required]
        public ICollection<DayOperationHours> operationHours { get; set; }
    }
}
