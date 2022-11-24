using App.Loading;
using App.ViewModels;
using App.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }
        private static LoadingPage loadingPage = new LoadingPage();

        public static async void Open(string text)
        {
            loadingPage.Message = text;
            if (PopupNavigation.Instance.PopupStack.Count == 0)
            {
                await PopupNavigation.Instance.PushAsync(loadingPage);
            }
        }
        public static async void Close()
        {
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            UserSession.Instancia.UsuarioActual = null;
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
