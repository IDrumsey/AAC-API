using AnimalAdoptionCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Resources
{
    public class DayOperationHoursResource
    {
        public DayOperationHoursResource(int dayOperationHoursId, DayNames day, List<SimpleTimeResource> times)
        {
            DayOperationHoursId = dayOperationHoursId;
            this.times = times;
            this.dayName = Enum.GetName<DayNames>(day);
        }

        public int DayOperationHoursId { get; set; }
        public string dayName { get; set; }
        //public DayNames day { get; set; }
        public List<SimpleTimeResource> times { get; set; }
    }
}
