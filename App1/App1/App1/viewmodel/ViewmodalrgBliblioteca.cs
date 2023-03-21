using App1.model;
using App1.view;
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
    internal class ViewmodalrgBliblioteca : INotifyPropertyChanged
    {

        public ViewmodalrgBliblioteca()
        {
           Cargar_Autores();
            Cargar_Editoriales();
            Cargar_Libros();
            Cargar_Prestamos();
            Registrar_Autor = new Command(async () =>
            {
                HttpClient cliente = new HttpClient();
                Autor registroAutor = new Autor
                {
                    nombre_autor = this.Nombre_Autor,
                    fecha = this.seleccion_fecha,
                };

                var Contenido = JsonConvert.SerializeObject(registroAutor);
                StringContent StringContenido = new StringContent(Contenido, Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync(urlautor, StringContenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Registro", "registro correcto", "Ok");
                    Cargar_Autores();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Registro", "error de envio", "Ok");
                }

            });
            Registrar_Editorial = new Command(async () =>
            {
                HttpClient cliente = new HttpClient();
                Editorial registroEditorial = new Editorial
                {
                    codigo = this.Codigo_Editorial,
                    nombre_editorial = this.Codigo_Editorial,
                };

                var Contenido = JsonConvert.SerializeObject(registroEditorial);
                StringContent StringContenido = new StringContent(Contenido, Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync(urleditorial, StringContenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Registro", "registro correcto", "Ok");
                    Cargar_Editoriales();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Registro", "error de envio", "Ok");
                }

            });
            Registrar_Libro = new Command(async () =>
            {
                HttpClient cliente = new HttpClient();
                Libro registroLibro = new Libro
                {
                    nombre_libro = this.Nombre_Libro,
                    id_autor = this.seleccion_autor.id_autor,
                    id_editorial = this.seleccion_editorial.id_editorial,
                    tema = this.Tema_Libro,
                    numero_edicion = this.Edicion_Libro,
                    observacion = this.Ob_Libro,
                    stock = this.Stock_Libro,


                };

                var Contenido = JsonConvert.SerializeObject(registroLibro);
                StringContent StringContenido = new StringContent(Contenido, Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync(urllibro, StringContenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Registro", "registro correcto", "Ok");
                    Cargar_Libros();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Registro", "error de envio", "Ok");
                }

            });
            Registrar_Prestamo = new Command(async () =>
            {
                HttpClient cliente = new HttpClient();
                Cabeceras registroPrestamo = new Cabeceras
                {
                    fecha_prestamo = this.fecha_ingreso,
                    fecha_devolucion = this.fecha_entrega,
                    id_usuario = 14,
                    id_libro = seleccion_libro.id_libro

                };

                var Contenido = JsonConvert.SerializeObject(registroPrestamo);
                StringContent StringContenido = new StringContent(Contenido, Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync(urllprestamo, StringContenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Registro", "registro correcto", "Ok");
                    Cargar_Prestamos();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Registro", "error de envio", "Ok");
                }

            });
        }


        string urlautor = "https://desfrlopez.me/dbarahona/api/autor/";
        string urleditorial = "https://desfrlopez.me/dbarahona/api/editorial";
        string urllibro = "https://desfrlopez.me/dbarahona/api/libros/";
        string urllprestamo = "https://desfrlopez.me/dbarahona/api/cabeceras/";
        string urllibrojoin = "https://desfrlopez.me/dbarahona/api/librosjoin/";
        string urlprestamojoin = "https://desfrlopez.me/dbarahona/api/cabecerasinner/";

        public DateTime seleccion_fecha { get; set; }
        public Autor Seleccion { get; set; }

        string nombre_autor_;
        public string Nombre_Autor
        {
            get => nombre_autor_;
            set
            {
                nombre_autor_ = value;
                var args = new PropertyChangedEventArgs(nameof(Nombre_Autor));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string nombre_editorial_;
        public string Nombre_Editorial
        {
            get => nombre_editorial_;
            set
            {
                nombre_editorial_ = value;
                var args = new PropertyChangedEventArgs(nameof(Nombre_Editorial));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string codigo_editorial;
        public string Codigo_Editorial
        {
            get => codigo_editorial;
            set
            {
                codigo_editorial = value;
                var args = new PropertyChangedEventArgs(nameof(Codigo_Editorial));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public async void Cargar_Autores()
        {
            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(urlautor);
            if (respuesta.IsSuccessStatusCode)
            {
                var aut= await respuesta.Content.ReadAsStringAsync();
                Autor_ = JsonConvert.DeserializeObject<ObservableCollection<Autor>>(aut);
            }
        }

        public async void Cargar_Editoriales()
        {
            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(urleditorial);
            if (respuesta.IsSuccessStatusCode)
            {
                var edit = await respuesta.Content.ReadAsStringAsync();
                Editorial_ = JsonConvert.DeserializeObject<ObservableCollection<Editorial>>(edit);
            }
        }

        public async void Cargar_Libros()
        {
            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(urllibrojoin);
            if (respuesta.IsSuccessStatusCode)
            {
                var lib = await respuesta.Content.ReadAsStringAsync();
                Libr_ = JsonConvert.DeserializeObject<ObservableCollection<Libro>>(lib);
            }
        }

        public async void Cargar_Prestamos()
        {
            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(urlprestamojoin);
            if (respuesta.IsSuccessStatusCode)
            {
                var lib = await respuesta.Content.ReadAsStringAsync();
                Pres_ = JsonConvert.DeserializeObject<ObservableCollection<Cabeceras>>(lib);

            }
        }

        ObservableCollection<Cabeceras> prestamos_ = new ObservableCollection<Cabeceras>();
        public ObservableCollection<Cabeceras> Pres_
        {
            get { return prestamos_; }
            set
            {
                prestamos_ = value;
                var args = new PropertyChangedEventArgs(nameof(Pres_));
                PropertyChanged?.Invoke(this, args);
            }
        }

        ObservableCollection<Libro> libros_ = new ObservableCollection<Libro>();
        public ObservableCollection<Libro> Libr_
        {
            get { return libros_; }
            set
            {
                libros_ = value;
                var args = new PropertyChangedEventArgs(nameof(Libr_));
                PropertyChanged?.Invoke(this, args);
            }
        }

        ObservableCollection<Autor> autores_ = new ObservableCollection<Autor>();
        public ObservableCollection<Autor> Autor_
        {
            get { return autores_; }
            set
            {
                autores_ = value;
                var args = new PropertyChangedEventArgs(nameof(Autor_));
                PropertyChanged?.Invoke(this, args);
            }
        }



        ObservableCollection<Editorial> editorial_ = new ObservableCollection<Editorial>();
        public ObservableCollection<Editorial> Editorial_
        {
            get { return editorial_; }
            set
            {
                editorial_ = value;
                var args = new PropertyChangedEventArgs(nameof(Editorial_));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public Autor seleccion_autor { get; set; }
        public Editorial seleccion_editorial { get; set; }
        public Libro seleccion_libro { get; set; }

        string nombre_libro_;
        public string Nombre_Libro
        {
            get => nombre_libro_;
            set
            {
                nombre_libro_ = value;
                var args = new PropertyChangedEventArgs(nameof(Nombre_Libro));
                PropertyChanged?.Invoke(this, args);
            }
        }
        int id_libro_;
        public int Id_Libro
        {
            get => id_libro_;
            set
            {
                id_libro_ = value;
                var args = new PropertyChangedEventArgs(nameof(Nombre_Libro));
                PropertyChanged?.Invoke(this, args);
            }
        }
        string tema_libro;
        public string Tema_Libro
        {
            get => tema_libro;
            set
            {
                tema_libro = value;
                var args = new PropertyChangedEventArgs(nameof(Tema_Libro));
                PropertyChanged?.Invoke(this, args);
            }
        }

        int edicion_libro;
        public int Edicion_Libro
        {
            get => edicion_libro;
            set
            {
                edicion_libro = value;
                var args = new PropertyChangedEventArgs(nameof(Edicion_Libro));
                PropertyChanged?.Invoke(this, args);
            }
        }

        string ob_libro;
        public string Ob_Libro
        {
            get => ob_libro;
            set
            {
                ob_libro = value;
                var args = new PropertyChangedEventArgs(nameof(Ob_Libro));
                PropertyChanged?.Invoke(this, args);
            }
        }

        int stock_libro;
        public int Stock_Libro
        {
            get => stock_libro;
            set
            {
                stock_libro = value;
                var args = new PropertyChangedEventArgs(nameof(Stock_Libro));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public int Seleccion_Ltabla { get; set; }


        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_entrega { get;set; }


        public Command Registrar_Autor { get; }
        public Command Registrar_Editorial { get; }
        public Command Registrar_Libro { get; }
        public Command Registrar_Prestamo { get; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
