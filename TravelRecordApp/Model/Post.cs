using Firebase.Database.Query;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
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

        private string experience;

        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                OnPropertyChanged("Experience");
            }
        }

        private string venueName;

        public string VenueName
        {
            get { return venueName; }
            set
            {
                venueName = value;
                OnPropertyChanged("VenueName");

            }
        }

        private string categoryID;

        public string CategoryID
        {
            get { return categoryID; }
            set
            {
                categoryID = value;
                OnPropertyChanged("CategoryID");
            }
        }

        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        private double longtitude;

        public double Longtitude
        {
            get { return longtitude; }
            set
            {
                longtitude = value;
                OnPropertyChanged("Longtitude");
            }
        }

        private DateTimeOffset dateTimeOffset;

        public DateTimeOffset CreatedAt
        {
            get { return dateTimeOffset; }
            set
            {
                dateTimeOffset = value;
                OnPropertyChanged("CreatedAt");

            }
        }

        public int UserId { get; set; }

        public Guid UserGuid { get; set; }

        private Venue venue;

        public Venue Venue
        {
            get { return venue; }
            set
            {
                venue = value;
                if (venue.categories != null)
                {
                    var firstCategory = venue.categories.FirstOrDefault();

                    if (firstCategory != null)
                    {
                        CategoryID = firstCategory.id;
                        CategoryName = firstCategory.name;
                    }
                }

                VenueName = venue.name;
                if (venue.location != null)
                {
                    Latitude = venue.location.lat;
                    Longtitude = venue.location.lng;
                    Address = venue.location.address;
                    Distances = venue.location.distance;
                }


                OnPropertyChanged("Venue");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public static async Task<List<Post>> Read()
        {
            var ttt = (await App.firebase.Child("Posts").OnceAsync<Post>());

            var posts = (await App.firebase.Child("Posts").OnceAsync<Post>())
                .Where(x => x.Object.UserGuid.ToString() == App.user.Guid.ToString())
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
                    CreatedAt = x.Object.CreatedAt

                }).ToList();

            return posts;
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    //conn.CreateTable<Post>();
            //    List<Post> posts = conn.Table<Post>().ToList();
            //    return posts;
            //}
        }

        public int Distances { get; set; }

        public static async Task<bool> Insert(Post post)
        {
            var row = await App.firebase.Child("Posts").PostAsync(post);
            if (row != null)
            {
                return true;
            }
            else
            {
                return false;
            }

            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Insert(post);

            //    if (rows > 0)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
        }

        public static Dictionary<string, int> GetCategories(List<Post> posts)
        {
            var categories = posts.OrderBy(x => x.CategoryID).Select(x => x.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoriesCOunt = new Dictionary<string, int>();

            foreach (var category in categories)
            {
                categoriesCOunt.Add(category, posts.Where(x => x.CategoryName == category).Count());
            }

            return categoriesCOunt;
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
