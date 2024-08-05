using SmartHome.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnMeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());

        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DevicesPage());
        }

        private async void OnAddDeviceButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDevicePage());
        }
    }
}