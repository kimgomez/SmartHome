using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartHome
{
    public partial class PasswordReset : ContentPage
    {
        private string _email;
        private string _token;

        public PasswordReset(string email, string token)
        {
            InitializeComponent();
            _email = email;
            _token = token;
        }

        private async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            var password = PasswordEntry.Text;
            var confirmPassword = ConfirmPasswordEntry.Text;

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                Email = _email,
                Token = _token,
                Password = password
            }), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://yourapiurl/api/account/reset-password", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Password has been reset.", "OK");
                // Navigate to login page
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to reset password. Please try again.", "OK");
            }
        }
    }
}
