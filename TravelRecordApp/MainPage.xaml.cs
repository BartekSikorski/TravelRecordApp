using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var assemby = typeof(MainPage);
            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assemby);
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {

            var isLogged = User.Login(emailEntry.Text, passwordEntry.Text);
            if (!isLogged)
            {
            } 
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}
