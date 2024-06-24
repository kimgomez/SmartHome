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
    public partial class ResetPassword : ContentPage
    {
        bool isPasswordVisible = false;
        bool isConfirmPasswordVisible = false;
        public ResetPassword()
        {
            InitializeComponent();
        }

        private void ResetPasswordButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtResetPassword.Text) || string.IsNullOrEmpty(txtResetPassword.Text) ||
                string.IsNullOrEmpty(txtConfirmNewPassword.Text) )
            {
                DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (txtResetPassword.Text != txtConfirmNewPassword.Text)
            {
                DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // Aquí puedes agregar la lógica para crear la cuenta, como guardar la información en una base de datos o llamar a un servicio web.

            DisplayAlert("Success", "Password Reset successfully!", "OK");
            Navigation.PopAsync(); // Regresar a la página anterior después de crear la cuenta
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

    }
}