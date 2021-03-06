﻿using Firebase.Database.Query;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
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

        private string userId;

        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }


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
            var posts = await FirebaseHelper.GetPosts(App.user);
            return posts;

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
