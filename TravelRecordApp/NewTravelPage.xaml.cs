using Plugin.Geolocator;
using System;
using System.Linq;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        Post post;
        public NewTravelPage()
        {
            InitializeComponent();
            post = new Post();
            containerStackLayout.BindingContext = post;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;

        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                post.VenueName = selectedVenue.name;
                post.Latitude = selectedVenue.location.lat;
                post.Longtitude = selectedVenue.location.lng;
                post.Address = selectedVenue.location.address;
                post.CategoryID = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.Distances = selectedVenue.location.distance;
                

                var isInserted = await Post.Insert(post);
                if (isInserted)
                {
                    await DisplayAlert("Success", "Experience added successfuly", "OK");
                }
                else
                {
                    await DisplayAlert("Failure", "Experience not added successfuly", "OK");
                }
            }
            catch(NullReferenceException nre)
            {
                await DisplayAlert("Failure", nre.Message, "OK");
                throw;

            }
            catch (Exception ex)
            {

                await DisplayAlert("Failure", ex.Message, "OK");
                throw;
            }
     
        }
    }
}