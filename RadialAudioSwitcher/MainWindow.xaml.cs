using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AudioSwitcher.AudioApi.CoreAudio;
using RadialMenu.Controls;

// USE THIS ONE FOR TRAY https://github.com/hardcodet/wpf-notifyicon

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
            item.Click += (s, e) => { device.SetAsDefaultAsync(); };
            return item;
        }

        private RadialMenuItem SoundPanelMenuItem()
        {
            var item = new RadialMenuItem { Content = new TextBlock { Text = "Sound Panel" } };
            item.Click += (s, e) => { CommandLineRunner.ExecuteCommandAsync("control mmsys.cpl sounds"); };
            return item;
        }
    }
}
