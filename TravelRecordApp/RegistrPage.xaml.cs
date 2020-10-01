using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrPage : ContentPage
    {
        private RegisterVM viewModel;
        public RegistrPage()
        {
            InitializeComponent();
            viewModel = new RegisterVM();
            BindingContext = viewModel;
        }
    }
}