using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;

namespace TravelRecordApp.Helpers
{
    public class FirebaseHelper
    {
        public static async Task<List<Post>> GetPosts(User user)
        {
            var posts = (await App.firebase.Child("Posts").OnceAsync<Post>())
                  //.Where(x => user.Guid != null ? x.Object.UserGuid.ToString() == user.Guid.ToString() : true)
                  .Where(x => user.Id != null ? x.Object.UserId == user.Id : true)
                  .Select(x => new Post
                  {
                      Experience = x.Object.Experience,
                      Latitude = x.Object.Latitude,
                      Longtitude = x.Object.Longtitude,
                      Venue = x.Object.Venue,
                      Address = x.Object.Address,
                      Distances = x.Object.Distances,
                      VenueName = x.Object.VenueName,
                      UserGuid = x.Object.UserGuid,
                      CreatedAt = x.Object.CreatedAt,
                      Guid = x.Object.Guid,
                      UserId = x.Object.UserId

                  }).ToList();

            return posts;
        }

        public static async Task<List<User>> GetUsers()
        {
           var users =  (await App.firebase.Child("Users").OnceAsync<User>()).Select(x => new User
            {
                Email = x.Object.Email,
                Password = x.Object.Password,
                Guid = x.Object.Guid
            }).ToList();

            return users;
        }
    }
}
