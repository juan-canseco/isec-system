using App.Loading;
using App.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vmLogin = null;
        public LoginPage()
        {
            InitializeComponent();
            txtUsername.Focus();
            this.BindingContext = vmLogin = new LoginViewModel(); 
        } 
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            App.Open("Ingresando...");
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    var login = await vmLogin.userService.Login(txtUsername.Text, txtPassword.Text);
                    if (login != null &&  login.Id >0)
                    {
                        UserSession.Instancia.UsuarioActual = login;
                        App.Close();
                        App.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        App.Close();
                        await App.Current.MainPage.DisplayAlert("AVISO!", "No existe el usuario, por favor verifique sus datos", "OK");
                    }
                }
                else
                {
                    App.Close();
                    await App.Current.MainPage.DisplayAlert("No hay coneccion", "No hay acceso a internet", "OK");
                }
            }
            catch (Exception ex)
            {
                App.Close();
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}