using SQLite;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public static List<Post> Read()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                List<Post> posts = conn.Table<Post>().ToList();
                return posts;
            }
        }

        public int Distances { get; set; }

        public static async Task<bool> Insert(Post post)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rows = conn.Insert(post);

                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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
