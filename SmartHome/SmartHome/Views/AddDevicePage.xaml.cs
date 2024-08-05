using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SmartHome.Views
{
    public partial class AddDevicePage : ContentPage
    {
        public ObservableCollection<Protocol> Protocols { get; set; }

        public AddDevicePage()
        {
            InitializeComponent();
            Protocols = new ObservableCollection<Protocol>
            {
                new Protocol { Name = "Zigbee", ImageSource = "Zigbee2.png" },
                new Protocol { Name = "Z-Wave", ImageSource = "Zwave.png" },
                new Protocol { Name = "WiFi", ImageSource = "wifi.png" },
                new Protocol { Name = "Bluetooth", ImageSource = "bluetooth.png" },
                new Protocol { Name = "Sonoff", ImageSource = "sonoff.png" }
                
            };

            BindingContext = this;
        }
    }

    public class Protocol
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
}
