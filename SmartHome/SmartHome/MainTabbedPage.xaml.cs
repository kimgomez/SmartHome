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
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
        }
        private async void OnAddDeviceClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Add Device", "Logic to add device goes here.", "OK");
        }

        private async void OnUserIconClicked(object sender, EventArgs e)
        {
            await DisplayAlert("User Profile", "Logic to navigate to user profile goes here.", "OK");
        }
    }
}