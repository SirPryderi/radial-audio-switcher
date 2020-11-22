using System;
using System.Windows;
using System.Windows.Input;

namespace RadialAudioSwitcher
{
    class AppTaskbarIconViewModel
    {
        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ShowWindowCommand => new DelegateCommand
        {
            CanExecuteFunc = () => !Application.Current.MainWindow.IsVisible,
            CommandAction = () => Application.Current.MainWindow.Show()
        };

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand => new DelegateCommand { 
            CommandAction = () => Application.Current.Shutdown() 
        };

        class DelegateCommand : ICommand
        {
            public Action CommandAction { get; set; }
            public Func<bool> CanExecuteFunc { get; set; }

            public void Execute(object parameter)
            {
                CommandAction();
            }

            public bool CanExecute(object parameter)
            {
                return CanExecuteFunc == null || CanExecuteFunc();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
