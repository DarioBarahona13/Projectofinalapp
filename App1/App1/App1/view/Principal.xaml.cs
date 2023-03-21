using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.viewmodel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {
        public Principal()
        {
            InitializeComponent();
            BindingContext = new Viewmodellogin();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrarAutor());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroEditorial());
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroLibro());
        }
        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroPrestamo());

        }
    }
}