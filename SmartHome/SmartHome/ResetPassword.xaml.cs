using SmartHome.Data;
using SmartHome.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPassword : ContentPage
    {
        bool isPasswordVisible = false;
        bool isConfirmPasswordVisible = false;
        private UserDatabase _database;
        public ResetPassword()
        {
            InitializeComponent();

        }
        public ResetPassword(UserDatabase database)
        {
            InitializeComponent();
            _database = database;
        }

        private async void ResetPasswordButton_Clicked(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtResetPassword.Text;
            string ConfirmPassword = txtResetPassword.Text;

            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Error", "Please enter your email.", "OK");
                return;
            }

            // Validación de caracteres seguros en la contraseña
            if (!IsValidPassword(password))
            {
                await DisplayAlert("Error", "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.", "OK");
                txtResetPassword.BackgroundColor = Color.Red;
                return;
            }

            // Validación de caracteres seguros en la contraseña
            if (!IsValidPassword(ConfirmPassword))
            {
                await DisplayAlert("Error", "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.", "OK");
                txtConfirmNewPassword.BackgroundColor = Color.Red;
                return;
            }

            // Validación de longitud mínima de la contraseña
            if (password.Length < 8)
            {
                await DisplayAlert("Error", "Password must be at least 8 characters long.", "OK");
                txtResetPassword.BackgroundColor = Color.Red;
                return;
            }

            // Validación de longitud mínima de la contraseña
            if (ConfirmPassword.Length < 8)
            {
                await DisplayAlert("Error", "Password must be at least 8 characters long.", "OK");
                txtConfirmNewPassword.BackgroundColor = Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(txtResetPassword.Text) || string.IsNullOrEmpty(txtResetPassword.Text) ||
                string.IsNullOrEmpty(txtConfirmNewPassword.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (password != ConfirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            //try
            //{
            //    var user = _database.GetUserByEmailAsync(email);
            //    if (user == null)
            //    {
            //        await DisplayAlert("Error", "User not found.", "OK");
            //        return;
            //    }


            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Error", ex.Message, "OK");
            //}

            var user = await _database.GetUserByEmailAsync(email);
            if (user == null)
            {
                await DisplayAlert("Error", "User not found. "+ email+"_ \n"+user ,"OK");
                return;
            }

            user.Password = password;
            await _database.UpdateUserAsync(user);

            await DisplayAlert("Success", "Password updated successfully.", "OK");

            // await DisplayAlert("Success", "Password Reset successfully!", "OK");
            await Navigation.PushAsync(new dbViewer());
            // await Navigation.PopAsync(); // Regresar a la página anterior después de crear la cuenta
        }
        private void OnTogglePasswordButtonClicked(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            txtResetPassword.IsPassword = !isPasswordVisible;
            btnTogglePassword.Source = isPasswordVisible ? "eye_closed_icon.png" : "eye_icon.png";
        }
        private void OnToggleConfirmPasswordButtonClicked(object sender, EventArgs e)
        {
            isConfirmPasswordVisible = !isConfirmPasswordVisible;
            txtConfirmNewPassword.IsPassword = !isConfirmPasswordVisible;
            btnToggleConfirmPassword.Source = isConfirmPasswordVisible ? "eye_closed_icon.png" : "eye_icon.png";
        }
        private bool IsValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }
    }
}