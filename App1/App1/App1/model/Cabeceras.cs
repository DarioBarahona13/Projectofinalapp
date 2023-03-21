using System;
using System.Collections.Generic;
using System.Text;

namespace App1.model
{
    public class Cabeceras
    {
        public int id_cabecera { get; set; }
        public DateTime fecha_prestamo { get; set; }  
        public DateTime fecha_devolucion { get; set; }

        public int id_usuario { get; set; } 
        public int id_libro { get; set; }   

        public string nombre_libro { get; set; }
    }
}
