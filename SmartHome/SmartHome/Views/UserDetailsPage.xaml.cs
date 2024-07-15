using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHome.Models;
using System;

namespace SmartHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailsPage : ContentPage
    {
        public UserDetailsPage(User user)
        {
            InitializeComponent();
            DisplayUserData(user);
        }

        private void DisplayUserData(User user)
        {
            FirstNameLabel.Text = user.FirstName;
            LastNameLabel.Text = user.LastName;
            EmailLabel.Text = user.Email;
        }

        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
