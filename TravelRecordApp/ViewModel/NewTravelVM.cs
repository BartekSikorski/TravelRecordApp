using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Command;

namespace TravelRecordApp.ViewModel
{
    public class NewTravelVM : INotifyPropertyChanged
    {
        public SaveCommand SaveCommand { get; set; }

        private Venue venue;

        private bool busy;

        public bool Busy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnProperyChanged("Busy");
            }
        }

        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                Post = new Post
                {
                    Experience = this.Experience,
                    Venue = this.Venue,
                    UserGuid = App.user.Guid,
                    UserId = App.user.Id
                };
                OnProperyChanged("Venue");
            }
        }

        private Post post;

        public Post Post
        {
            get { return post; }
            set
            {
                post = value;
                OnProperyChanged("Post");
            }
        }

        private string experience;

        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                Post = new Post
                {
                    Experience = this.Experience,
                    Venue = this.Venue
                };
                OnProperyChanged("Experience");
            }
        }


        public NewTravelVM()
        {
            SaveCommand = new SaveCommand(this);
            Venue = new Venue();
            Post = new Post();
            Busy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnProperyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async void Save(Post post)
        {
            using (UserDialogs.Instance.Loading("Waiting..."))
            {
                try
                {
                    post.UserGuid = App.user.Guid;
                    post.CreatedAt = DateTimeOffset.Now;
                    post.UserId = App.user.Id;
                    Busy = true;
                    var isInserted = await Post.Insert(post);
                    if (isInserted)
                    {
                        Busy = false;
                        await App.Current.MainPage.DisplayAlert("Success", "Experience added successfuly", "OK");
                    }
                    else
                    {
                        Busy = false;

                        await App.Current.MainPage.DisplayAlert("Failure", "Experience not added successfuly", "OK");
                    }
                }
                catch (NullReferenceException nre)
                {
                    await App.Current.MainPage.DisplayAlert("Failure", nre.Message, "OK");
                    throw;

                }
                catch (Exception ex)
                {
                    Busy = false;
                    await App.Current.MainPage.DisplayAlert("Failure", ex.Message, "OK");
                    throw;
                }
            }     
        }
    }
}
