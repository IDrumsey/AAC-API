using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoptionCenterModels
{
    public class SimpleTime
    {
        public SimpleTime(int simpleTimeId, int dayOperationHoursId, IntervalSide intervalSide, int hours, int minutes, int seconds)
        {
            SimpleTimeId = simpleTimeId;
            DayOperationHoursId = dayOperationHoursId;
            this.intervalSide = intervalSide;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public int SimpleTimeId { get; set; }
        public int DayOperationHoursId { get; set; }
        public IntervalSide intervalSide { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
    }
}
