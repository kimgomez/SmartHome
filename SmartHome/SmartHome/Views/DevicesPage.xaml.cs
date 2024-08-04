using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHome.Views
{
    public partial class DevicesPage : ContentPage
    {
        private bool isLightOn = false;
        private bool isVehicleOn = false;
        private bool isAirConditionerOn = false;
        private bool isCameraOn = false;
        private bool isVacuumOn = false;
        private bool isWaterPumpOn = false;
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
                await SendCommandToDevice("192.168.11.38", "Power%20On");
            }
            else
            {
                ToggleSwitch.Text = "Off";
                ToggleSwitch.BackgroundColor = Color.Red;
                LightbulbImage.Source = "OFF.png";
                await SendCommandToDevice("192.168.11.38", "Power%20Off");
            }
        }

        private async void OnVehicleToggleClicked(object sender, EventArgs e)
        {
            isVehicleOn = !isVehicleOn;

            if (isVehicleOn)
            {
                VehicleToggle.Text = "On";
                VehicleToggle.BackgroundColor = Color.Green;
                VehicleImage.Source = "VehicleOn.png"; // Supone que tienes una imagen para el estado "On"
                await SendCommandToDevice("192.168.11.39", "Power%20On");
            }
            else
            {
                VehicleToggle.Text = "Off";
                VehicleToggle.BackgroundColor = Color.Red;
                VehicleImage.Source = "Vehicle.png";
                await SendCommandToDevice("192.168.11.39", "Power%20Off");
            }
        }

        private async void OnAirConditionerToggleClicked(object sender, EventArgs e)
        {
            isAirConditionerOn = !isAirConditionerOn;

            if (isAirConditionerOn)
            {
                AirConditionerToggle.Text = "On";
                AirConditionerToggle.BackgroundColor = Color.Green;
                AirConditionerImage.Source = "AirConditionerOn.png";
                await SendCommandToDevice("192.168.11.40", "Power%20On");
            }
            else
            {
                AirConditionerToggle.Text = "Off";
                AirConditionerToggle.BackgroundColor = Color.Red;
                AirConditionerImage.Source = "AirConditioner.png";
                await SendCommandToDevice("192.168.11.40", "Power%20Off");
            }
        }

        private async void OnCameraToggleClicked(object sender, EventArgs e)
        {
            isCameraOn = !isCameraOn;

            if (isCameraOn)
            {
                CameraToggle.Text = "View";
                CameraToggle.BackgroundColor = Color.Green;
                CameraImage.Source = "CameraOn.png";
                await SendCommandToDevice("192.168.11.41", "Power%20On");
            }
            else
            {
                CameraToggle.Text = "Photo";
                CameraToggle.BackgroundColor = Color.Red;
                CameraImage.Source = "Camera.png";
                await SendCommandToDevice("192.168.11.41", "Power%20Off");
            }
        }

        private async void OnVacuumToggleClicked(object sender, EventArgs e)
        {
            isVacuumOn = !isVacuumOn;

            if (isVacuumOn)
            {
                VacuumToggle.Text = "On";
                VacuumToggle.BackgroundColor = Color.Green;
                VacuumImage.Source = "VacuumOn.png";
                await SendCommandToDevice("192.168.11.42", "Power%20On");
            }
            else
            {
                VacuumToggle.Text = "Off";
                VacuumToggle.BackgroundColor = Color.Red;
                VacuumImage.Source = "Vacuum.png";
                await SendCommandToDevice("192.168.11.42", "Power%20Off");
            }
        }

        private async void OnWaterPumpToggleClicked(object sender, EventArgs e)
        {
            isWaterPumpOn = !isWaterPumpOn;

            if (isWaterPumpOn)
            {
                WaterPumpToggle.Text = "On";
                WaterPumpToggle.BackgroundColor = Color.Green;
                WaterPumpImage.Source = "WaterPumpOn.png";
                await SendCommandToDevice("192.168.11.43", "Power%20On");
            }
            else
            {
                WaterPumpToggle.Text = "Off";
                WaterPumpToggle.BackgroundColor = Color.Red;
                WaterPumpImage.Source = "WaterPump.png";
                await SendCommandToDevice("192.168.11.43", "Power%20Off");
            }
        }

        private async Task SendCommandToDevice(string ipAddress, string command)
        {
            try
            {
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
