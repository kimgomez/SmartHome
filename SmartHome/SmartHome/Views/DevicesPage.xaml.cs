using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicesPage : ContentPage
	{
        private bool isLightOn = false;
        public DevicesPage ()
		{
			InitializeComponent ();
		}

        private void OnToggleSwitchClicked(object sender, EventArgs e)
        {
            isLightOn = !isLightOn;

            if (isLightOn)
            {
                ToggleSwitch.Text = "On";
                ToggleSwitch.BackgroundColor = Color.Green;
                // Aquí es donde enviarás el comando para encender el dispositivo
            }
            else
            {
                ToggleSwitch.Text = "Off";
                ToggleSwitch.BackgroundColor = Color.Red;
                // Aquí es donde enviarás el comando para apagar el dispositivo
            }
        }
       }
}