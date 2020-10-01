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
                    UserGuid = App.user.Guid
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
            try
            {
                post.UserGuid = App.user.Guid;
                post.CreatedAt = DateTimeOffset.Now;
                var isInserted = await Post.Insert(post);
                if (isInserted)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Experience added successfuly", "OK");
                }
                else
                {
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

                await App.Current.MainPage.DisplayAlert("Failure", ex.Message, "OK");
                throw;
            }
        }
    }
}
