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
    public partial class Login : ContentPage
    {
        bool isPasswordVisible = false;
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(txtUsername.Text=="admin" && txtPassword.Text == "123")
            {
                Navigation.PushAsync(new HomePage());
            } else
            {
                DisplayAlert("Ops..", "Usuario o Clave incorrecta", "Ok");
                txtUsername.BackgroundColor = Color.Red;
                txtPassword.BackgroundColor = Color.Red;
            }
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
            // Aquí puedes agregar la lógica para navegar a una página de recuperación de contraseña
            DisplayAlert("Forgot Password", "Redirigir a la página de recuperación de contraseña.", "OK");
        }
    }
}