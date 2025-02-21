using fe_hunt_gui.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fe_hunt_gui
{
    /// <summary>
    /// Interaktionslogik für Titlebar.xaml
    /// </summary>
    public partial class Titlebar : UserControl
    {
        private bool _isDarkMode = false;
        
        public Titlebar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Toggles the application theme between dark mode and light mode.
        /// </summary>
        private void ToggleThemeButton_Click(object sender, RoutedEventArgs e)
        {
            _isDarkMode = !_isDarkMode;
            string theme = _isDarkMode ? "DarkMode" : "LightMode";
            ((App)Application.Current).ApplyTheme(theme);
        }

        /// <summary>
        /// Minimizes the parent window.
        /// </summary>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the parent window and Minimize it
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }
        }

        /// <summary>
        /// Closes the parent window and stops Docker Compose processes.
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            HelperMethodes.StopDockerCompose();
            HelperMethodes.KillProcessById(ProcessManager.BEdockerComposeID);
            HelperMethodes.KillProcessById(ProcessManager.CurrentCmdID);

            // Get the parent window and close it
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
    }
}
