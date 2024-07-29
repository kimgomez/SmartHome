using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            // Cargar la información del usuario
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            // Ejemplo de cómo cargar la información del usuario, ajusta esto según tu lógica
            FirstNameLabel.Text = "Juan";
            LastNameLabel.Text = "Corniel";
            EmailLabel.Text = "gabrielcorniel@gmail.com";
        }

        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            // Navegar a la página de cambio de contraseña (a crear más adelante)
            await Navigation.PushAsync(new ResetPassword());
        }

        private async void OnBackToHomeClicked(object sender, EventArgs e)
        {
            // Lógica para regresar a la HomePage
            await Navigation.PushAsync(new HomePage());
        }

        private async void OnExitAppClicked(object sender, EventArgs e)
        {
            // Lógica para salir de la aplicación
            //System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            await Navigation.PushAsync(new Login());
            
        }
    }
}
