using App1.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using App1.view;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace App1.viewmodel
{
    public class Viewmodellogin : INotifyPropertyChanged
    {
        public Viewmodellogin()
        {
            InicioSesion = new Command(async () =>
            {
                HttpClient cliente = new HttpClient();
                var urlcompleta = url + Nombre_Usuario + "/" + Password;
                  var respuesta = await cliente.GetAsync(urlcompleta);
                  if (respuesta.IsSuccessStatusCode)
                  {
                      var contenido = await respuesta.Content.ReadAsStringAsync();
                      var inicio = JsonConvert.DeserializeObject<List<login>>(contenido);
                      if (inicio[0].is_valid == 1)
                      {
                          await App.Current.MainPage.Navigation.PushAsync(new Principal() { }); 
                      }
                      else
                      {
                          await Application.Current.MainPage.DisplayAlert("Inicio Sesion", "error", "Ok");
                      }
                  }
            });

        }
        public async void datos_usuario()
        {
            HttpClient cliente = new HttpClient();
            var urlusuario = url_datosUsuario + Nombre_Usuario;
            HttpResponseMessage respuesta = await cliente.GetAsync(urlusuario);
            if (respuesta.IsSuccessStatusCode)
            {
                var datosstring = await respuesta.Content.ReadAsStringAsync();
                var datos = JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(datosstring);
                User_ = new ObservableCollection<Usuario>(datos);
            }
        }

        ObservableCollection<Usuario> user_ = new ObservableCollection<Usuario>();
        public ObservableCollection<Usuario> User_
        {
            get => user_;
            set
            {
                user_ = value;
                var args = new PropertyChangedEventArgs(nameof(User_));
                PropertyChanged?.Invoke(this, args);
            }
        }

        Usuario usuario_ = new Usuario();
        public Usuario Usuario_
        {
            get => usuario_;
            set
            {
                usuario_ = value;
                var args = new PropertyChangedEventArgs(nameof(Usuario));
                PropertyChanged?.Invoke(this, args);
            }
        }



        string url_datosUsuario = "https://desfrlopez.me/dbarahona/api/usuarioinner/";

        string url = "https://desfrlopez.me/dbarahona/api/login/";

        public string nombre_usuario;
        public string Nombre_Usuario
        {
            get => nombre_usuario;
            set
            {
                nombre_usuario = value;
                var args = new PropertyChangedEventArgs(nameof(Password));
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

        public Command InicioSesion { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }


}
