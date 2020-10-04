using Acr.UserDialogs;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel
{
    public class HistoryVM
    {
        public  ObservableCollection<Post> Posts { get; set; }

        public HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
        }
        public async void UpdatePosts()
        {
            using (UserDialogs.Instance.Loading("Waiting for history update..."))
            {
                var posts = await Post.Read();
                Posts.Clear();
                foreach (var post in posts)
                {
                    Posts.Add(post);
                }
            }
        }

        public async void DeletePost(Post post)
        {
            var toDeletePost = (await App.firebase
               .Child("Posts")
               .OnceAsync<Post>()).FirstOrDefault(a => a.Object.Guid == post.Guid);

            await App.firebase
                .Child("Posts")
                .Child(toDeletePost.Key)
                .DeleteAsync();
        }
    }
}
