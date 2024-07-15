using System;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SmartHome.Models;
using System.Threading.Tasks;
using Xamarin.Auth.Presenters;
using SmartHome.Views;
using System.Text.RegularExpressions;

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
                //Validacion de campos vacios
                DisplayAlert("Error", "Please fill in all fields and agree to the privacy policy.", "OK");
                return;
            }

            if (txtCreatePassword.Text != txtConfirmPassword.Text)
            {
                DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // Validacion del formato de email 
            if (!IsValidEmail(txtEmail.Text))
            {
                 DisplayAlert("Error", "Please enter a valid email address.", "OK");
                txtEmail.BackgroundColor = Color.Red;
                return;
            }

            // Validacion de longitud minima de la contraseña
            if (txtCreatePassword.Text.Length < 8)
            {
                 DisplayAlert("Error", "Password must be at least 8 characters long.", "OK");
                txtCreatePassword.BackgroundColor = Color.Red;
                return;
            }
            // Validacion de longitud minima de confirmar contraseña
            if (txtConfirmPassword.Text.Length < 8)
            {
                DisplayAlert("Error", "Password must be at least 8 characters long.", "OK");
                txtConfirmPassword.BackgroundColor = Color.Red;
                return;
            }

            //Validacion de caracteres en la contraseña
            if (!IsValidPassword(txtCreatePassword.Text))
            {
                 DisplayAlert("Error", "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.", "OK");
                txtCreatePassword.BackgroundColor = Color.Red;
                return;
            }

            //Validacion de caracteres en confirmar contraseña
            if (!IsValidPassword(txtConfirmPassword.Text))
            {
                DisplayAlert("Error", "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.", "OK");
                txtConfirmPassword.BackgroundColor = Color.Red;
                return;
            }

            DisplayAlert("Success", "Account created successfully!", "OK");
            //Navigation.PushAsync(new MainPage());
            Navigation.PopAsync(); // Regresar a la página anterior después de crear la cuenta
        }

        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        private bool IsValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
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
                    await Navigation.PushAsync(new HomePage());
                    // Mostrar ventana de detalles del usuario
                    await Navigation.PushModalAsync(new UserDetailsPage(user));
                }
                else
                {
                    await DisplayAlert("Authentication", "Failed to retrieve user info", "OK");
                }
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
