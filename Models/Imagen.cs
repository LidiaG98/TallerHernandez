using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TallerHernandez.Models
{
    public class Imagen
    {
        public int idImagen { get; set; }
        public String nombreImagen { get; set; }
        public String imagePath { get; set; }
        public String perteneceA { get; set; }
        public String idDuenio { get; set; }
        public IFormFile imageFile { get; set; }

    }
}
