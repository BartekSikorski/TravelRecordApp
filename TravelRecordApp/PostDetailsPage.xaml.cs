using Firebase.Database.Query;
using SQLite;
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
    public partial class PostDetailsPage : ContentPage
    {
        Post selectedPost;
        public PostDetailsPage(Post postDetails)
        {
            InitializeComponent();
            this.selectedPost = postDetails;
            ExperienceEntry.Text = postDetails.Experience;
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = ExperienceEntry.Text;


            var allPosts = (await App.firebase
               .Child("Posts").OnceAsync<Post>());

            var toUpdatePost = (await App.firebase
               .Child("Posts")
               .OnceAsync<Post>()).FirstOrDefault(a => a.Object.Guid == selectedPost.Guid);

            await App.firebase
                .Child("Posts")
                .Child(toUpdatePost.Key)
                .PutAsync(selectedPost);


            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Update(selectedPost);
            //    if (rows > 0)
            //    {
            //        DisplayAlert("Success", "Updated successfully", "OK");
            //    } else
            //    {
            //        DisplayAlert("Failure", "Updated failure", "OK");

            //    }
            //}
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {

            var toDeletePost = (await App.firebase
               .Child("Posts")
               .OnceAsync<Post>()).FirstOrDefault(a => a.Object.Guid == selectedPost.Guid);

            await App.firebase
                .Child("Posts")
                .Child(toDeletePost.Key)
                .DeleteAsync();

            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Delete(selectedPost);

            //    if (rows > 0)
            //    {
            //        DisplayAlert("Success", "Deleted successfully", "OK");
            //    }
            //    else
            //    {
            //        DisplayAlert("Failure", "Delete failure", "OK");

            //    }
            //}

        }
    }
}