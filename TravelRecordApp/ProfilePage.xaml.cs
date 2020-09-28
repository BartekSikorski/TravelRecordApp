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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var postTable = conn.Table<Post>().ToList();

                var categories = postTable.OrderBy(x => x.CategoryID).Select(x => x.CategoryName).Distinct().ToList();

                Dictionary<string, int> categoriesCOunt = new Dictionary<string, int>();

                foreach (var category in categories)
                {
                    categoriesCOunt.Add(category, postTable.Where(x => x.CategoryName == category).Count());
                }

                categoriesListView.ItemsSource = categoriesCOunt;

                postCountLabel.Text = postTable.Count().ToString();

            }
        }
    }
}