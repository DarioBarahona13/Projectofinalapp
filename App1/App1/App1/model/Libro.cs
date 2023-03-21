using System;
using System.Collections.Generic;
using System.Text;

namespace App1.model
{
    public class Libro
    {
        public int id_libro {  get; set; }
        public string nombre_libro { get; set; }
        public int id_autor { get; set; }
        public int id_editorial { get; set; }
        public string tema { get; set; }
        public int numero_edicion { get; set; }
        public string observacion { get; set; }
        public int stock { get; set; } 
    }
}
