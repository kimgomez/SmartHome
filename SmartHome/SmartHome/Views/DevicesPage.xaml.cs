using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHome.Views
{
    public partial class DevicesPage : ContentPage
    {
        private bool isLightOn = false;
        private readonly HttpClient _httpClient;

        public DevicesPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnToggleSwitchClicked(object sender, EventArgs e)
        {
            isLightOn = !isLightOn;

            if (isLightOn)
            {
                ToggleSwitch.Text = "On";
                ToggleSwitch.BackgroundColor = Color.Green;
                LightbulbImage.Source = "On.png";
                await SendCommandToDevice("Power%20On");
            }
            else
            {
                ToggleSwitch.Text = "Off";
                ToggleSwitch.BackgroundColor = Color.Red;
                LightbulbImage.Source = "OFF.png";
                await SendCommandToDevice("Power%20Off");
            }
        }

        private async Task SendCommandToDevice(string command)
        {
            try
            {
                var ipAddress = "192.168.11.38"; 
                var url = $"http://{ipAddress}/cm?cmnd={command}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // El comando fue exitoso
                }
                else
                {
                    // Manejar errores aquí
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí
                Console.WriteLine($"Error al enviar comando: {ex.Message}");
            }
        }
    }
}
