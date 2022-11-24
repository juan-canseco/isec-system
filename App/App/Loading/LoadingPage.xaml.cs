using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : PopupPage
    {
        public string Message = string.Empty;
        public LoadingPage()
        {
            InitializeComponent();
            string msg = Message;
            lblMessage.Text = msg;
        }
        protected override bool OnBackButtonPressed()
        {
            return false;
        }
        protected override bool OnBackgroundClicked()
        {
            return false;
        }
        private void PopupPage_Appearing(object sender, EventArgs e)
        {
            lblMessage.Text = Message;
        }
    }
}