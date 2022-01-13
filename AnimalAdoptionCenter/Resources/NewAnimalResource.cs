using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Resources
{
    public class NewAnimalResource : UpdatedAnimalResource
    {
        public override int storeId { get; set; }
    }
}
