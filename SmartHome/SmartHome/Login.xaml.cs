using System;
using SmartHome.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private BiometricAuthenticationService _biometricService;

        public Login()
        {
            InitializeComponent();
            _biometricService = new BiometricAuthenticationService();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var isAuthenticated = await _biometricService.AuthenticateAsync();

            if (isAuthenticated)
            {
                // Usuario autenticado mediante biometría
                await DisplayAlert("Éxito", "Autenticación biométrica exitosa", "OK");
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                // Intentar autenticación con usuario y contraseña
                if (txtUsername.Text == "admin" && txtPassword.Text == "123")
                {
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Ops..", "Usuario o Clave incorrecta", "Ok");
                    txtUsername.BackgroundColor = Color.Red;
                    txtPassword.BackgroundColor = Color.Red;
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}
