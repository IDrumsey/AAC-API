using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Extensions
{
    public static class ModelStateExtensions
    {
        
        public static List<string> GetAllErrorMessages(this ModelStateDictionary dictionary)
        {
            var models = dictionary.Values;
            var errors = new List<ModelErrorCollection>();
            foreach (var model in models)
            {
                errors.Add(model.Errors);
            }

            List<string> errorMsgs = new List<string>();

            foreach(var errorCollection in errors)
            {
                foreach (var error in errorCollection)
                {
                    errorMsgs.Add(error.ErrorMessage);
                }
            }

            return errorMsgs;
        }
    }
}
