using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoptionCenterModels
{
    public class SavedFile
    {
        // https://www.codemag.com/Article/1901061/Upload-Small-Files-to-a-Web-API-Using-Angular
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public long lastModified { get; set; }
        public string asBase64 { get; set; }
        public byte[] asByteArray { get; set; }
    }
}
