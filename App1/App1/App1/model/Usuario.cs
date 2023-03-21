using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.model
{
    public class Usuario
    {

        public int id_usuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string nombre_usuario { get; set; }
        public string password { get; set; }
        public int id_role { get; set; }
        public string role { get; set; }

       /* public string JsonData()
        {
            return "{\"id_usuario\": \"" + id_usuario + "\",\"nombres\": \"" + nombres + "\",\"apellidos\": \"" + apellidos + "\",\"nombre_usuario\": \"" + usuario + "\",\"password\": \"" + contra + "\",\"id_role\": \"" + id_role + "\",\"role\": \"" + role + "\"}";
        }*/
    }
}
