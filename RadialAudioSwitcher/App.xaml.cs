using Ownskit.Utils;
using System;
using System.Windows;

namespace RadialAudioSwitcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly KeyboardListener kListener = new KeyboardListener();
        private MainWindow window = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Console.WriteLine("Application Started");
            kListener.KeyUp += new RawKeyEventHandler(OnKeyUp);
            Current.Dispatcher.Invoke(delegate { window = new MainWindow(); });
        }

        private void OnKeyUp(object sender, RawKeyEventArgs args)
        {
            Console.WriteLine(args.Key.ToString());
            // Console.WriteLine(args.ToString()); // Prints the text of pressed button, takes in account big and small letters. E.g. "Shift+a" => "A"

            var key = args.Key.ToString();

            switch (key)
            {
                case "Pause":
                    if (window.IsVisible) window.CloseMenuAndHide();
                    else window.Show();
                    break;
                case "Escape":
                    if (window.IsVisible) window.CloseMenuAndHide();
                    break;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            kListener.Dispose();
            base.OnExit(e);
        }
    }
}
