using fe_hunt_gui.Manager;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace fe_hunt_gui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //hunt.local in pi hole or frizbox hinzufügen... etc
            ElementsManager.StartDocker = StartDocker;

            ElementsManager.EditorColumn = EditorColumn;
            ElementsManager.Editor_Button = Editor_Button;

            ElementsManager.Play_Button = Play_Button;
            ElementsManager.PlayColumn = PlayColumn;

            ElementsManager.Titlebar = TitleBar;
            ElementsManager.LoadingCircle = LoadingCircle;
            ElementsManager.Exit_Button = Exit_Button;

            HelperMethodes.StartDocker();

            //Create .env, appsettings and Logfile
            HelperMethodes.CreateFile("C:\\ProgramData\\Hunt\\Logfile", "\\logfile.txt", "");
            HelperMethodes.CreateEnvFiles();
            HelperMethodes.CreateAppsettings();
            HelperMethodes.CreateIpConfigs();

            //Standart DarkMode
            ((App)Application.Current).ApplyTheme("DarkMode");
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event to allow window dragging.
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Handles the click event for the Editor button.
        /// </summary>
        private void Editor_Button_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start Docker and BE
                HelperMethodes.StartDocker();
                HelperMethodes.Start_BE_Hunt();

                ElementsManager.DisableAllButtons();
                ElementsManager.LoadingCircle.Visibility = Visibility.Visible;
                ElementsManager.Exit_Button.Visibility = Visibility.Visible;
                Grid.SetColumn(LoadingCircle, 0);
                Grid.SetColumn(Exit_Button, 0);
                ElementsManager.LoadingCircle.Height = 120;
                ElementsManager.LoadingCircle.Width = 120;

                ProcessManager.CurrentCmdProcess = new Process();
                ProcessManager.CurrentCmdProcess.StartInfo.FileName = "cmd.exe";
                ProcessManager.CurrentCmdProcess.StartInfo.Arguments = "/K " + "npm ci && npm run build && npm run preview -- --open"; //"npm ci && npm run dev -- --open"
                ProcessManager.CurrentCmdProcess.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Hunt\\HSAA_Projektarbeit\\src\\fe-hunt-editor";
                ProcessManager.CurrentCmdProcess.StartInfo.UseShellExecute = true;
                ProcessManager.CurrentCmdProcess.StartInfo.CreateNoWindow = true;
                //ProcessManager.CurrentCmdProcess.StartInfo.Verb = "runas";

                // Start the process and keep the cmd window open
                ProcessManager.CurrentCmdProcess.Start();
                ProcessManager.CurrentCmdID = ProcessManager.CurrentCmdProcess.Id;

            }
            catch (Exception ex)
            {
                Logfile.WriteLog($"Edit: Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the click event for the Web Game button.
        /// </summary>
        private void Web_Game_Button_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                //Start Docker and BE
                HelperMethodes.StartDocker();
                HelperMethodes.Start_BE_Hunt();

                ElementsManager.DisableAllButtons();
                ElementsManager.LoadingCircle.Visibility = Visibility.Visible;
                ElementsManager.Exit_Button.Visibility = Visibility.Visible;
                //ElementsManager.LoadingCircle.Height = 140;
                //ElementsManager.LoadingCircle.Width = 140;
                Grid.SetColumn(LoadingCircle, 2);
                Grid.SetColumn(Exit_Button, 2);
                ElementsManager.LoadingCircle.Height = 120;
                ElementsManager.LoadingCircle.Width = 120;

                ProcessManager.CurrentCmdProcess = new Process();
                ProcessManager.CurrentCmdProcess.StartInfo.FileName = "cmd.exe";
                ProcessManager.CurrentCmdProcess.StartInfo.Arguments = "/K " + "npm ci && npm run build && npm run preview -- --open --host"; // "npm ci && npm run dev -- --open";
                ProcessManager.CurrentCmdProcess.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Hunt\\HSAA_Projektarbeit\\src\\fe-hunt-web-game";
                ProcessManager.CurrentCmdProcess.StartInfo.UseShellExecute = true;
                ProcessManager.CurrentCmdProcess.StartInfo.CreateNoWindow = true;
                //ProcessManager.CurrentCmdProcess.StartInfo.Verb = "runas";

                ProcessManager.CurrentCmdProcess.Start();
                ProcessManager.CurrentCmdID = ProcessManager.CurrentCmdProcess.Id;

            }
            catch (Exception ex)
            {
                Logfile.WriteLog($"Partic.: Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the click event for the Options button.
        /// </summary>
        private void Options_Button_Clicked(object sender, RoutedEventArgs e)
        {
            //Options If needed, currently not used
        }

        /// <summary>
        /// Handles the click event for the Exit button.
        /// </summary>
        private void Exit_Button_Clicked(object sender, RoutedEventArgs e)
        {
            HelperMethodes.KillProcessById(ProcessManager.CurrentCmdID);

            ElementsManager.LoadingCircle.Visibility = Visibility.Hidden;
            ElementsManager.Exit_Button.Visibility = Visibility.Hidden;
            ElementsManager.EnableAllButtons();
        }
    }
}
