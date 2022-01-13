using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Resources
{
    public class UpdatedAnimalResource
    {
        public string name { get; set; } = null;
        [Range(0, Int32.MaxValue, ErrorMessage = "must be a non-negative number")]
        public int age { get; set; }
        public char gender { get; set; }
        public string classificationName { get; set; } = null;
        public string species { get; set; } = null;
        [Range(0, Int32.MaxValue, ErrorMessage = "must be a non-negative number")]
        public int heightInches { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "must be a non-negative number")]
        public int weight { get; set; }
        public string favoriteToy { get; set; } = null;
        public string favoriteActivity { get; set; } = null;
        public string description { get; set; } = null;
        [Range(0, Int32.MaxValue, ErrorMessage = "must be a non-negative number")]
        public virtual int storeId { get; set; }

        public bool hasDefinedEmptyStringProperty()
        {
            List<string> propertiesToCheck = new List<string> { this.name, this.classificationName, this.species, this.favoriteToy, this.favoriteActivity, this.description };

            // check if each value is defined then if it is an empty string
            foreach (var property in propertiesToCheck)
            {
                if(property != null)
                {
                    if(property == "")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<string> findDefinedEmptyStrings()
        {
            List<string> propertiesToCheck = new List<string> { this.name, this.classificationName, this.species, this.favoriteToy, this.favoriteActivity, this.description };

            List<string> definedEmptyStringProperties = new List<string>();

            for (int i = 0; i < propertiesToCheck.Count; i++)
            {
                if (propertiesToCheck[i] != null)
                {
                    if (propertiesToCheck[i] == "")
                    {
                        if (i == 0)
                        {
                            definedEmptyStringProperties.Add("name");
                        }
                        if (i == 1)
                        {
                            definedEmptyStringProperties.Add("classificationName");
                        }
                        if (i == 2)
                        {
                            definedEmptyStringProperties.Add("species");
                        }
                        if (i == 3)
                        {
                            definedEmptyStringProperties.Add("favoriteToy");
                        }
                        if (i == 4)
                        {
                            definedEmptyStringProperties.Add("favoriteActivity");
                        }
                        if (i == 5)
                        {
                            definedEmptyStringProperties.Add("description");
                        }
                    }
                }
            }

            return definedEmptyStringProperties;
        }

        public string GetEmptyStringErrorMsg(string emptyVariableName)
        {
            return $"{emptyVariableName} can't be an empty string.";
        }

        public string GetEmptyStringErrors()
        {
            var hasEmptyStringError = this.hasDefinedEmptyStringProperty();

            if (hasEmptyStringError)
            {
                var emptyStrings = this.findDefinedEmptyStrings();

                string errorMessages = "";

                foreach (var emptyString in emptyStrings)
                {
                    errorMessages += this.GetEmptyStringErrorMsg(emptyString) + "\n";
                }

                return errorMessages;
            }
            else
            {
                return null;
            }
        }
    }
}
