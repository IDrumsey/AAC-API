using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoptionCenterModels
{
    public class Store
    {
        public int StoreId { get; set; }
        public string address { get; set; }
        public ICollection<Animal> Animals { get; set; }
        public ICollection<DayOperationHours> operationHours { get; set; }
        public List<LocationPicture> pictures { get; set; }
    }
}
