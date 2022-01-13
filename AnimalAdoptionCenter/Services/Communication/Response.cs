using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalAdoptionCenter.Services.Communication
{
    public class Response<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        // successful response
        public Response(T data)
        {
            this.success = true;
            this.message = null;
            this.data = data;
        }

        // failed response
        public Response(string message)
        {
            this.success = false;
            this.message = message;
        }
    }
}
