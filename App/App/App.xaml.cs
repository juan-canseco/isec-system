using App.Loading;
using App.Services;
using App.Views;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    public partial class App : Application
    {


        private static LoadingPage loadingPage = null;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new LoginPage();
            loadingPage = new LoadingPage();
        } 
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
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
