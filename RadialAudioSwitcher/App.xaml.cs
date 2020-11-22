using Hardcodet.Wpf.TaskbarNotification;
using Ownskit.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RadialAudioSwitcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new MainWindow MainWindow { get; set; }
        
        private readonly KeyboardListener kListener = new KeyboardListener();
        private TaskbarIcon icon = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            icon = (TaskbarIcon) FindResource("AppTaskbarIcon");
            Current.Dispatcher.Invoke(delegate { MainWindow = new MainWindow(); });
            kListener.KeyUp += new RawKeyEventHandler(OnKeyUp);
            Console.WriteLine("Application Started");
        }

        private void OnKeyUp(object sender, RawKeyEventArgs args)
        {
            Console.WriteLine(args.Key.ToString());
            // Console.WriteLine(args.ToString()); // Prints the text of pressed button, takes in account big and small letters. E.g. "Shift+a" => "A"

            var key = args.Key.ToString();

            switch (key)
            {
                case "Pause":
                    if (MainWindow.IsVisible) MainWindow.CloseMenuAndHide();
                    else MainWindow.Show();
                    break;
                case "Escape":
                    if (MainWindow.IsVisible) MainWindow.CloseMenuAndHide();
                    break;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            icon.Dispose();
            kListener.Dispose();
            base.OnExit(e);
        }
    }
}
