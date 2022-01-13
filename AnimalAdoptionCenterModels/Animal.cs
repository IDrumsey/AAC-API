using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoptionCenterModels
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public char gender { get; set; }
        public string classificationName { get; set; }
        public string species { get; set; }
        public int heightInches { get; set; }
        public int weight { get; set; }
        public string favoriteToy { get; set; }
        public string favoriteActivity { get; set; }
        public string description { get; set; }
        public int storeId { get; set; }
        public List<FileName> pictures { get; set; } = new List<FileName>();
    }
}
