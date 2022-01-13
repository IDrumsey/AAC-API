using AnimalAdoptionCenterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Resources
{
    public class SimpleTimeResource
    {
        public SimpleTimeResource(int simpleTimeId, IntervalSide intervalSide, int hours, int minutes, int seconds)
        {
            SimpleTimeId = simpleTimeId;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;

            this.intervalSideName = Enum.GetName<IntervalSide>(intervalSide);
        }

        public int SimpleTimeId { get; set; }
        public string intervalSideName { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
    }
}
