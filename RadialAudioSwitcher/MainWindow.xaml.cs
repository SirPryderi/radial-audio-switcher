using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AudioSwitcher.AudioApi.CoreAudio;

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
            var index = 0;
            var devices = await coreAudioController.GetPlaybackDevicesAsync();
            foreach (var device in devices)
            {
                if (device.State != AudioSwitcher.AudioApi.DeviceState.Active) continue;
                // Console.WriteLine(device.FullName+ index);
                // Console.WriteLine(device.IconPath);
                // System.Drawing.Icon p = System.Drawing.Icon.ExtractAssociatedIcon(path);
                var btn = new Button { Content = device.FullName };
                btn.Click += (s, e) => {
                    device.SetAsDefaultAsync();
                };

                Grid.SetColumn(btn, index);
                Grid.SetRow(btn, index);
                
                grid.Children.Add(btn);
                grid.RowDefinitions.Add(new RowDefinition());
                
                index++;
            }
        }        
    }
}
