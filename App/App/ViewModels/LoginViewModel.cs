using App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using App.Service;
using Refit;
using Xamarin.Essentials;
namespace App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public  IUserService userService;
        public Action evento { get; set; }
        public AccesoViewModel acceso { get; set; }
        public LoginViewModel()
        {
            //LoginCommand = new Command(evento);
            userService = RestService.For<IUserService>(Urls.ApiUrl); 
        }

        private async  void OnLoginClicked(object obj)
        { 
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //await Shell.Current.GoToAsync($"//{nameof(Home)}");
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var login = await userService.Login("", "");
                if (login != null)
                {
                    App.Current.MainPage = new AppShell(); 
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("AVISO!", "No existe el usuario, por favor verifique sus datos", "OK");

                }

            }
            else
            {
               await  App.Current.MainPage.DisplayAlert("No hay coneccion", "No hay acceso a internet", "OK");
            } 
        }
    }
}
