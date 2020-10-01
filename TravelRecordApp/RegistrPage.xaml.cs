using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrPage : ContentPage
    {
        public RegistrPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (passwordEntry.Text == confirmPasswordEntry.Text)
                {
                    var user = new User()
                    {
                        Email = emailEntry.Text,
                        Password = passwordEntry.Text

                    };

                    //FirebaseClient firebase = new FirebaseClient("https://travel-record-app-290815.firebaseio.com/");

                    var newuser = await App.firebase.Child("Users").PostAsync(user);

                }
                else
                {
                    DisplayAlert("Error", "Password's don't match", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
          
        }
    }
}