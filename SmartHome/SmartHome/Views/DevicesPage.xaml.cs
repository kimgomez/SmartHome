using System;
using Xamarin.Forms;

namespace SmartHome.Views
{
    public partial class DevicesPage : ContentPage
    {
        private bool isLightOn = false;

        public DevicesPage()
        {
            InitializeComponent();
        }

        private void OnToggleSwitchClicked(object sender, EventArgs e)
        {
            isLightOn = !isLightOn;

            if (isLightOn)
            {
                ToggleSwitch.Text = "On";
                ToggleSwitch.BackgroundColor = Color.Green;
                LightbulbImage.Source = "On.png";
                // Aquí es donde enviarás el comando para encender el dispositivo
            }
            else
            {
                ToggleSwitch.Text = "Off";
                ToggleSwitch.BackgroundColor = Color.Red;
                LightbulbImage.Source = "OFF.png";
                // Aquí es donde enviarás el comando para apagar el dispositivo
            }
        }
    }
}
