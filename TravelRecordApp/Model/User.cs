using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{
    public class User : INotifyPropertyChanged
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        private string id;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
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
                OnPropertyChanged("ConfirmPassword");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public static async Task<string> Login(string userName, string password)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBawVnNH00j5nmTKZxEadDKUGXkI7qde3o"));
                var token = await authProvider.SignInWithEmailAndPasswordAsync(userName, password);
                App.user = new User { Id = token.User.LocalId };
                return "OK";

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
           
            //if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            //{
            //    return false;
            //}
            //else
            //{
            //    var users = await FirebaseHelper.GetUsers();

            //    var user = users.Where(x => x.Email == userName).FirstOrDefault();

            //    if (user != null)
            //    {

            //        if (user.Password == password)
            //        {
            //            App.user = user;
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }

            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
