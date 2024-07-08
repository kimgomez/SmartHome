using System;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Auth.Presenters;

namespace SmartHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        bool isPasswordVisible = false;
        bool isConfirmPasswordVisible = false;

        public Registro()
        {
            InitializeComponent();
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Poner el foco en el campo txtFirstName
            txtFirstName.Focus();
        }

        private void CreateAccountButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtCreatePassword.Text) ||
                string.IsNullOrEmpty(txtConfirmPassword.Text) || !chkAgree.IsChecked)
            {
                DisplayAlert("Error", "Please fill in all fields and agree to the privacy policy.", "OK");
                return;
            }

            if (txtCreatePassword.Text != txtConfirmPassword.Text)
            {
                DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            DisplayAlert("Success", "Account created successfully!", "OK");
            Navigation.PopAsync(); // Regresar a la página anterior después de crear la cuenta
        }

        private void OnTogglePasswordButtonClicked(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtCreatePassword.IsPassword = !isPasswordVisible;
            btnTogglePassword.Source = isPasswordVisible ? "eye_closed_icon.png" : "eye_icon.png";
        }

        private void OnToggleConfirmPasswordButtonClicked(object sender, EventArgs e)
        {
            isConfirmPasswordVisible = !isConfirmPasswordVisible;
            txtConfirmPassword.IsPassword = !isConfirmPasswordVisible;
            btnToggleConfirmPassword.Source = isConfirmPasswordVisible ? "eye_closed_icon.png" : "eye_icon.png";
        }

        private void OnGoogleLoginClicked(object sender, EventArgs e)
        {
            try
            {
                var authenticator = new OAuth2Authenticator(
                    clientId: "516982201725-46ftjb6ocp4dr3a6ecoi9a730r9aho2i.apps.googleusercontent.com",
                    clientSecret: null, // Google doesn't use client secret for mobile apps
                    authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth"),
                    accessTokenUrl: new Uri("https://accounts.google.com/o/oauth2/token"),
                    redirectUrl: new Uri("https://accounts.google.com"),
                    scope: "email",
                    isUsingNativeUI: true); // Use native UI if available

                if (authenticator == null)
                {
                    DisplayAlert("Error", "Authenticator is null", "OK");
                    return;
                }

                authenticator.Completed += OnAuthCompleted;
                authenticator.Error += OnAuthError;

                AuthenticationState.Authenticator = authenticator;

                var presenter = new OAuthLoginPresenter();
                if (presenter == null)
                {
                    DisplayAlert("Error", "Presenter is null", "OK");
                    return;
                }

                presenter.Login(authenticator);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void OnFacebookLoginClicked(object sender, EventArgs e)
        {
            var authenticator = new OAuth2Authenticator(
                clientId: "1189944522132451",
                scope: "email",
                authorizeUrl: new Uri("https://www.facebook.com/dialog/oauth"),
                redirectUrl: new Uri("https://m.facebook.com"),
                isUsingNativeUI: true); // Use native UI if available

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var token = e.Account.Properties["access_token"];
                await DisplayAlert("Authentication", "Login successful!", "OK");
            }
            else
            {
                await DisplayAlert("Authentication", "Login failed!", "OK");
            }
        }

        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            DisplayAlert("Authentication", "Login error: " + e.Message, "OK");
        }
    }

    public static class AuthenticationState
    {
        public static OAuth2Authenticator Authenticator;
    }
}
