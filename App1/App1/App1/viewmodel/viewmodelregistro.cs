using App1.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace App1.viewmodel
{
    public class viewmodelregistro : INotifyPropertyChanged
    {
        public viewmodelregistro()
        {
            Carga_roles();
             Registrar = new Command(async () =>
             {
                 HttpClient cliente = new HttpClient();
                 Usuario registroDatos = new Usuario
                 {
                     nombres = this.Nombres,
                     apellidos = this.Apellidos,
                     nombre_usuario = this.Nombre_Usuario,
                     password = this.Password,
                     id_role = Seleccion_role.id_role
                 };



                 var Contenido = JsonConvert.SerializeObject(registroDatos);
                 StringContent StringContenido = new StringContent(Contenido, Encoding.UTF8, "application/json");
                 var respuesta = await cliente.PostAsync(url_post, StringContenido);
                 if (respuesta.IsSuccessStatusCode)
                 {
                     await Application.Current.MainPage.DisplayAlert("Registro", "registro correcto", "Ok");
                     await App.Current.MainPage.Navigation.PopAsync();
                 }
                 else
                 {
                     await Application.Current.MainPage.DisplayAlert("Registro", "error de envio", "Ok");
                 }
             });
        }


        string url_post = "https://desfrlopez.me/dbarahona/api/usuarios/";
        string url_role = "https://desfrlopez.me/dbarahona/api/roles";


        public async void Carga_roles()
        {
            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(url_role);
            if (respuesta.IsSuccessStatusCode)
            {
                var rols = await respuesta.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<RolesUsuario>>(rols);
                Roles = new ObservableCollection<RolesUsuario>(datos);
            }
        }

        ObservableCollection<RolesUsuario> roles = new ObservableCollection<RolesUsuario>();
        public ObservableCollection<RolesUsuario> Roles
        {
            get => roles;
            set
            {
                roles = value;
                var args = new PropertyChangedEventArgs(nameof(Roles));
                PropertyChanged?.Invoke(this, args);
            }

        }



        string nombres;
        public string Nombres
        {
            get => nombres;
            set
            {
                nombres = value;
                var args = new PropertyChangedEventArgs(nameof(Nombres));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string apellidos;
        public string Apellidos
        {
            get => apellidos;
            set
            {
                apellidos = value;
                var args = new PropertyChangedEventArgs(nameof(Apellidos));
                PropertyChanged?.Invoke(this, args);

            }
        }

        string nombre_usuario;
        public string Nombre_Usuario
        {
            get => nombre_usuario;
            set
            {
                nombre_usuario = value;
                var args = new PropertyChangedEventArgs(nameof(Nombre_Usuario));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                var args = new PropertyChangedEventArgs(nameof(Password));
                PropertyChanged?.Invoke(this, args);
            }
        }

        int id_role;
        public int Id_Role
        {
            get => id_role;
            set
            {
                id_role = value;
                var args = new PropertyChangedEventArgs(nameof(Id_Role));
                PropertyChanged?.Invoke(this, args);

            }
        }


        

        public Command Registrar { get; set; }

        public RolesUsuario Seleccion_role { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
