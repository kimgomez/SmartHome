using System;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHome.Models;
using System.Threading.Tasks;
using Xamarin.Auth.Presenters;
using SmartHome.Views;

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

                authenticator.Completed += OnAuthCompleted;
                authenticator.Error += OnAuthError;

                AuthenticationState.Authenticator = authenticator;

                var presenter = new OAuthLoginPresenter();
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

            var presenter = new OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var token = e.Account.Properties["access_token"];
                await SecureStorage.SetAsync("access_token", token);
                await Navigation.PushAsync(new HomePage());
                // Obtener información del usuario
                var userInfo = await GetUserInfoAsync(token);


                // Llenar los campos del formulario
                if (userInfo != null)
                {
                    var user = new User
                    {
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        Email = userInfo.Email,
                        Token = token
                    };
                    await App.Database.SaveUserAsync(user);
                    // await Navigation.PushAsync(new HomePage());
                    // Mostrar ventana de detalles del usuario
                    await Navigation.PushModalAsync(new UserDetailsPage(user));
                    // Retraso antes de la redirección
                  

                    // Redirigir a la página HomePage
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    

                    // Redirigir a la página HomePage
                    await Navigation.PushAsync(new HomePage());
                    //await DisplayAlert("Authentication", "Failed to retrieve user info", "OK");
                }
            }
            else
            {
               
                // Redirigir a la página HomePage
                await Navigation.PushAsync(new HomePage());
                //await DisplayAlert("Authentication", "Failed to retrieve user info", "OK");
                //await DisplayAlert("Authentication", "Login failed!", "OK");
            }
        }

        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            DisplayAlert("Authentication", "Login error: " + e.Message, "OK");
        }

        private async Task<UserInfo> GetUserInfoAsync(string token)
        {
            // Lógica para obtener información del usuario usando el token
            // Implementa esto según el proveedor de OAuth que estás usando
            return new UserInfo
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };
        }
    }

    public class UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public static class AuthenticationState
    {
        public static OAuth2Authenticator Authenticator;
    }
}
