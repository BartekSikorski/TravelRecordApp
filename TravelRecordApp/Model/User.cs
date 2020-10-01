using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class User : INotifyPropertyChanged
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        private int id;

        public int Id
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

        public static async Task<bool> Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            else
            {
                var users = (await App.firebase.Child("Users").OnceAsync<User>()).Select(x => new User
                {
                    Email = x.Object.Email,
                    Password = x.Object.Password,
                    Guid = x.Object.Guid
                }).ToList();

                var user = users.Where(x => x.Email == userName).FirstOrDefault();

                if (user != null)
                {

                    if (user.Password == password)
                    {
                        App.user = user;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
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
