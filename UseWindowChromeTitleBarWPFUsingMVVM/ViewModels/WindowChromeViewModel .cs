using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UseWindowChromeTitleBarWPFUsingMVVM.ViewModels
{
    public class WindowChromeViewModel : INotifyPropertyChanged
    {
        public ICommand WindowStateChangedCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand RestoreWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        private Thickness mainBorderThickness = new Thickness(1);
        public Thickness MainBorderThickness
        {
            get { return mainBorderThickness; }
            set
            {
                mainBorderThickness = value;
                OnPropertyChanged(nameof(MainBorderThickness));
            }
        }

        private Visibility restoreButtonVisibility = Visibility.Collapsed;

        public Visibility RestoreButtonVisibility
        {
            get { return restoreButtonVisibility; }
            set
            {
                restoreButtonVisibility = value;
                OnPropertyChanged(nameof(RestoreButtonVisibility));
            }
        }

        private Visibility maximizeButtonVisibility = Visibility.Visible;

        public Visibility MaximizeButtonVisibility
        {
            get { return maximizeButtonVisibility; }
            set
            {
                maximizeButtonVisibility = value;
                OnPropertyChanged(nameof(MaximizeButtonVisibility));
            }
        }

        public WindowChromeViewModel()
        {
            WindowStateChangedCommand = new WindowChromeCommand(Execute_WindowStateChangedCommand);
            MaximizeWindowCommand = new WindowChromeCommand(Execute_MaximizeWindowCommand);
            RestoreWindowCommand = new WindowChromeCommand(Execute_RestoreWindowCommand);
            MinimizeWindowCommand = new WindowChromeCommand(Execute_MinimizeWindowCommand);
            CloseWindowCommand = new WindowChromeCommand(Execute_CloseWindowCommand);
        }

        private void Execute_WindowStateChangedCommand()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                MainBorderThickness = new Thickness(8);
                RestoreButtonVisibility = Visibility.Visible;
                MaximizeButtonVisibility = Visibility.Collapsed;
            }
            else
            {
                MainBorderThickness = new Thickness(0);
                RestoreButtonVisibility = Visibility.Collapsed;
                MaximizeButtonVisibility = Visibility.Visible;
            }
        }

        private void Execute_MaximizeWindowCommand()
        {
            if (Application.Current.MainWindow != null)
            {
                SystemCommands.MaximizeWindow(Application.Current.MainWindow);
            }
        }
        private void Execute_MinimizeWindowCommand()
        {
            if (Application.Current.MainWindow != null)
            {
                SystemCommands.MinimizeWindow(Application.Current.MainWindow);
            }
        }
        private void Execute_RestoreWindowCommand()
        {
            if (Application.Current.MainWindow != null)
            {
                SystemCommands.RestoreWindow(Application.Current.MainWindow);
            }
        }
        private void Execute_CloseWindowCommand()
        {
            if (Application.Current.MainWindow != null)
            {
                SystemCommands.CloseWindow(Application.Current.MainWindow);
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion //INotifyPropertyChanged
    }
}