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
    }
}
