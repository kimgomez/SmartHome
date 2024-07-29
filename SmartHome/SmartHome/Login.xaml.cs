using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using SmartHome.Data;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        bool isPasswordVisible = false;
        UserDatabase _database;
        private readonly ClassSmartHome _databaseService;
        public Login()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserSQLite.db3");
            _database = new UserDatabase(dbPath);
            //_databaseService = new ClassSmartHome("DESKTOP-JVUM7P0", "SmartHome", "your_user", "your_password");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //  Foco en el campo User
            txtUsername.Focus();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }

        private void OnTogglePasswordButtonClicked(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtPassword.IsPassword = !isPasswordVisible;
            btnTogglePassword.Source = isPasswordVisible ? "eye_closed_icon.png" : "eye_icon.png";
        }

        private void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResetPassword());

            // DisplayAlert("Forgot Password", "Redirigir a la página de recuperación de contraseña.", "OK");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Both username and password are required.", "OK");
                txtUsername.BackgroundColor = Color.Red;
                txtPassword.BackgroundColor = Color.Red;
                return;
            }

            //// Validación del formato del correo electrónico (si aplica)
            //if (!IsValidEmail(username))
            //{
            //    await DisplayAlert("Error", "Please enter a valid email address.", "OK");
            //    txtUsername.BackgroundColor = Color.Red;
            //    return;
            //}

            //// Validación de longitud mínima de la contraseña
            //if (password.Length < 8)
            //{
            //    await DisplayAlert("Error", "Password must be at least 8 characters long.", "OK");
            //    txtPassword.BackgroundColor = Color.Red;
            //    return;
            //}

            // Validación de caracteres seguros en la contraseña
            //if (!IsValidPassword(password))
            //{
            //    await DisplayAlert("Error", "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.", "OK");
            //    txtPassword.BackgroundColor = Color.Red;
            //    return;
            //}

            // Si todas las validaciones pasan, procede con el inicio de sesión

            string email = txtUsername.Text;
            string passw = txtPassword.Text;

            // Verifica las credenciales (suponiendo que has almacenado la contraseña como un hash seguro)
            var user = (await _database.GetUsersAsync()).FirstOrDefault(u => u.FirstName == username && u.Password == password);

            if (user != null)
            {
                await DisplayAlert("Success", $"Welcome, {user.FirstName}!", "OK");
                // Navegar a la página principal de la aplicación
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password. Please try again.", "OK");
            }


            //if (username == "admin" && password == "123")
            //{
            //    await Navigation.PushAsync(new HomePage());
            //}
            //else
            //{
            //    await DisplayAlert("Ops..", "Usuario o Clave incorrecta", "Ok");
            //    txtUsername.BackgroundColor = Color.Red;
            //    txtPassword.BackgroundColor = Color.Red;
            //}
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

        public async Task<bool> AuthenticateAsync()
        {
            var availability = await CrossFingerprint.Current.IsAvailableAsync();
            if (!availability)
            {
                await DisplayAlert("Error", "El sensor de huella dactilar no está disponible o no está configurado.", "OK");
                return false;
            }

            var config = new AuthenticationRequestConfiguration("Prove your identity", "Please authenticate")
            {
                CancelTitle = "Cancelar",
                FallbackTitle = "Usar código de acceso"
            };

            var result = await CrossFingerprint.Current.AuthenticateAsync(config);
            return result.Authenticated;
        }

        private async void OnFingerprintLoginClicked(object sender, EventArgs e)
        {
            var isAuthenticated = await AuthenticateAsync();
            if (isAuthenticated)
            {
                // Autenticación exitosa, proceder con el inicio de sesión
                await DisplayAlert("Éxito", "Autenticación exitosa", "OK");
                // Navegar a la página principal de la aplicación o realizar cualquier acción necesaria
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                // Autenticación fallida, mostrar mensaje de error
                await DisplayAlert("Error", "Autenticación fallida", "OK");
            }
        }
        public async Task<bool> AuthenticateAsync(string nombreUsuario, string passw)
        {
            string query = "SELECT COUNT(1) FROM login WHERE nombreUsuario = @nombreUsuario AND passw = @passw";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@nombreUsuario", nombreUsuario),
            new SqlParameter("@passw", passw)
            };

            int count = await _databaseService.ExecuteScalarAsync(query, parameters);
            return count == 1;
        }



        //// Llama a este método cuando necesites autenticar al usuario
        //public async void OnLoginButtonClicked()
        //{
        //    var isAuthenticated = await AuthenticateAsync();
        //    if (isAuthenticated)
        //    {
        //        // Autenticación exitosa, proceder con el inicio de sesión
        //        await DisplayAlert("Success", "Authenticated successfully", "OK");
        //    }
        //    else
        //    {
        //        // Autenticación fallida, mostrar mensaje de error
        //        await DisplayAlert("Error", "Authentication failed", "OK");
        //    }
        //}
    }
}