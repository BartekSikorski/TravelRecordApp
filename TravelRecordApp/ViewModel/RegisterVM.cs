using Acr.UserDialogs;
using Firebase.Auth;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Command;

namespace TravelRecordApp.ViewModel
{
    public class RegisterVM : INotifyPropertyChanged
    {

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                User = new Model.User
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new Model.User
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new Model.User
                {
                    Email = this.Email,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        private Model.User user;

        public Model.User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public RegisterCommand RegisterCommand { get; set; }

        public RegisterVM()
        {
            RegisterCommand = new RegisterCommand(this);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async void Register(Model.User user)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBawVnNH00j5nmTKZxEadDKUGXkI7qde3o"));
                var token = await authProvider.CreateUserWithEmailAndPasswordAsync(user.Email, user.Password);
                UserDialogs.Instance.Toast("User added successfuly");
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {

                UserDialogs.Instance.Alert(ex.Message);
            }
          

            //var newuser = await App.firebase.Child("Users").PostAsync(user);
        }
    }
}

