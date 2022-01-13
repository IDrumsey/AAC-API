using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoptionCenterModels
{
    public class DayOperationHours
    {
        public int DayOperationHoursId { get; set; }
        public DayNames day { get; set; }
        public List<SimpleTime> times { get; set; }
        public int StoreId { get; set; }
    }
}
