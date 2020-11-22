using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AudioSwitcher.AudioApi.CoreAudio;
using RadialMenu.Controls;

namespace RadialAudioSwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CoreAudioController coreAudioController = null;

        public MainWindow()
        {
            InitializeComponent();
            coreAudioController = new CoreAudioController();
            AddButtons();

            Deactivated += (a, b) => { CloseMenuAndHide(); };

            IsVisibleChanged += (a, b) =>
            {
                if (IsVisible)
                {
                    Activate();
                    RadialMenu.IsOpen = true;
                }
            };
        }

        private async void AddButtons()
        {
            var devices = await coreAudioController.GetPlaybackDevicesAsync();

            List<RadialMenuItem> items = new List<RadialMenuItem>();

            foreach (var device in devices)
            {
                if (device.State != AudioSwitcher.AudioApi.DeviceState.Active) continue;
                // System.Drawing.Icon p = System.Drawing.Icon.ExtractAssociatedIcon(path);  
                items.Add(MenuItemForDevice(device));
            }

            items.Add(SoundPanelMenuItem());

            RadialMenu.Items = items;
        }

        private RadialMenuItem MenuItemForDevice(CoreAudioDevice device)
        {
            var item = new RadialMenuItem { Content = new TextBlock { Text = device.Name } };
            item.IsEnabled = !device.IsDefaultDevice;
            item.Click += (s, e) =>
            {
                device.SetAsDefaultAsync();
                RadialMenu.Items.ForEach((i) => i.IsEnabled = true);
                item.IsEnabled = false;
            };
            return item;
        }

        private RadialMenuItem SoundPanelMenuItem()
        {
            var item = new RadialMenuItem { Content = new TextBlock { Text = "Sound Panel" } };
            item.Click += (s, e) => { CommandLineRunner.ExecuteCommandAsync("control mmsys.cpl sounds"); };
            return item;
        }

        public async void CloseMenuAndHide(object sender = null, RoutedEventArgs e = null)
        {
            if (!RadialMenu.IsOpen) return;
            Console.WriteLine("Close menu");
            RadialMenu.IsOpen = false;
            ShowActivated = false;
            await Task.Delay(500);
            Console.WriteLine("Hide window");
            Hide();
        }
    }
}
