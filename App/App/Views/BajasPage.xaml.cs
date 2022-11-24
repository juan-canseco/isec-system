using App.Service;
using DataAccess;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BajasPage : ContentPage
    {
        Usuario user = UserSession.Instancia.UsuarioActual; 
        IUserService userService;
        public BajasPage()
        {
            InitializeComponent();
            userService = RestService.For<IUserService>(Urls.ApiUrl); 
        }
        private async void ContentPage_Appearing(object sender, EventArgs e)
        { 
            cUsers.ItemsSource = await userService.all();
        }
    }
}