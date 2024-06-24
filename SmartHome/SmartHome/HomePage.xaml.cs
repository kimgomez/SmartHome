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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        //private void OnAddDeviceClicked(object sender, EventArgs e)
        //{
        //    // Aquí puedes agregar la lógica para agregar un nuevo dispositivo
        //    DisplayAlert("Add Device", "Logic to add device goes here.", "OK");
        //}

        //private void OnUserIconClicked(object sender, EventArgs e)
        //{
        //    // Aquí puedes agregar la lógica para navegar a la página de perfil de usuario
        //    DisplayAlert("User Profile", "Logic to navigate to user profile goes here.", "OK");
        //}
    }
}