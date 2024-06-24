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
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
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

            // Aquí puedes agregar la lógica para crear la cuenta, como guardar la información en una base de datos o llamar a un servicio web.

            DisplayAlert("Success", "Account created successfully!", "OK");
            Navigation.PopAsync(); // Regresar a la página anterior después de crear la cuenta
        }
    }
}