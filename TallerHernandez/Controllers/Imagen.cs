using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Controllers
{
    public class  Imagen
    {
        public string nombreImagen { get; set; }
        public IFormFile imageFile { get; set; }
    }
}
