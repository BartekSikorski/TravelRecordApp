using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Logic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;

        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();
                Post post = new Post
                {
                    Experience = Experience.Text,
                    VenueName = selectedVenue.name,
                    Latitude = selectedVenue.location.lat,
                    Longtitude = selectedVenue.location.lng,
                    Address = selectedVenue.location.address,
                    CategoryID = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Distances = selectedVenue.location.distance
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Post>();
                    int rows = conn.Insert(post);

                    if (rows > 0)
                    {
                        DisplayAlert("Success", "Experience added successfuly", "OK");
                    }
                    else
                    {
                        DisplayAlert("Failure", "Experience not added successfuly", "OK");
                    }
                }
            }
            catch(NullReferenceException nre)
            {

            }
            catch (Exception ex)
            {

                throw;
            }
     
        }
    }
}