using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace SmartHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordRecovery : ContentPage
    {
        public PasswordRecovery()
        {
            InitializeComponent();
        }

        // ForgotPasswordPage.xaml.cs

            private async void OnSendResetLinkClicked(object sender, EventArgs e)
            {
                var email = EmailEntry.Text;

                var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(new { Email = email }), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://yourapiurl/api/account/forgot-password", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Password reset link sent to your email.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to send reset link. Please try again.", "OK");
                }
            }
        }
    }