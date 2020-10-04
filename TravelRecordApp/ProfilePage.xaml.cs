using Acr.UserDialogs;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            using (UserDialogs.Instance.Loading("Waiting for profile info..."))
            {
                var postTable = await Post.Read();

                categoriesListView.ItemsSource = Post.GetCategories(postTable);

                postCountLabel.Text = postTable.Count().ToString();
            }


        }
    }
}