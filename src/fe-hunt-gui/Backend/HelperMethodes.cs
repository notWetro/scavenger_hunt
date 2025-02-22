using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using fe_hunt_gui.Manager;
using System.Windows.Threading;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Windows.Controls;



namespace fe_hunt_gui
{
    public class HelperMethodes
    {
        /// <summary>
        /// Starts the Docker process and updates the UI elements accordingly.
        /// </summary>
        public static void StartDocker()
        {
            try
            {
                ElementsManager.StartDocker.Visibility = Visibility.Visible;
                ElementsManager.LoadingCircle.Visibility = Visibility.Visible;
                Grid.SetColumn(ElementsManager.LoadingCircle, 1);

                ElementsManager.LoadingCircle.Height = 160;
                ElementsManager.LoadingCircle.Width = 160;


                //ElementsManager.Editor_Button.Visibility = Visibility.Hidden;
                //ElementsManager.Play_Button.Visibility = Visibility.Hidden;

                if (IsProcessRunning("Docker Desktop", null))
                {
                    Logfile.WriteLog("Docker process is already running.");
                    ElementsManager.StartDocker.Visibility = System.Windows.Visibility.Hidden;
                    ElementsManager.LoadingCircle.Visibility = System.Windows.Visibility.Hidden;
                    return;
                }

                var p = new Process();

                string path = HelperMethodes.GetDockerDesktopPath();
                if (path == null || !System.IO.File.Exists(path))
                {
                    path = HelperMethodes.FindDockerDesktopPath();
                }

                //p.StartInfo.Verb = "runas"; // Start as Admin

                p.StartInfo.FileName = path;
                p.StartInfo.CreateNoWindow = false;
                p.Start();

                p.WaitForExit();

                DispatcherTimer _timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(10) // Set the duration (in seconds)
                };

                // When the timer ticks, hide the element
                _timer.Tick += (sender, e) =>
                {
                    ElementsManager.StartDocker.Visibility = Visibility.Hidden; // Hide the element
                    ElementsManager.LoadingCircle.Visibility = Visibility.Hidden; // Hide the element
                    //ElementsManager.Editor_Button.Visibility = Visibility.Visible;
                    //ElementsManager.Play_Button.Visibility = Visibility.Visible;
                    ElementsManager.LoadingCircle.Height = 120;
                    ElementsManager.LoadingCircle.Width = 120;
                    _timer.Stop(); // Stop the timer
                };

                // Start the timer
                _timer.Start();

            }
            catch (InvalidOperationException ex)
            {
                Logfile.WriteLog($"Docker not found: {ex.Message}");
                MessageBox.Show("Please read The Readme and Download, Install Docker: 'https://www.docker.com' before opening the app again!");
                
                // Get the parent window and close it
                Window parentWindow = Window.GetWindow(ElementsManager.Titlebar);
                if (parentWindow != null)
                {
                    parentWindow.Close();
                }
            }
            catch (Exception ex)
            {
                Logfile.WriteLog($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Starts the backend Hunt API using docker-compose.
        /// </summary>
        public static void Start_BE_Hunt()
        {
            try
            {
                string pathBE = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Hunt\\HSAA_Projektarbeit\\src\\be-hunt-api\\Hunts.Api";

                // Check if docker-compose is already running
                if (IsProcessRunning("cmd", "docker-compose up --build"))
                {
                    Logfile.WriteLog("The docker-compose process is already running.");
                    return;
                }

                // Otherwise, start the process
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe"; // Use cmd.exe for command prompt
                process.StartInfo.Arguments = "/K " + "docker-compose up --build"; // "/K" keeps the cmd window open after executing the commands
                process.StartInfo.WorkingDirectory = pathBE;
                process.StartInfo.UseShellExecute = true; // Use shell execute to show the window
                process.StartInfo.CreateNoWindow = false; // Show the window
                //process.StartInfo.Verb = "runas";

                // Start the process and keep the cmd window open
                process.Start();
                ProcessManager.BEdockerComposeID = process.Id;

            }
            catch (Exception ex)
            {
                Logfile.WriteLog($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates the appsettings.json files for the Hunts and Participants APIs.
        /// </summary>
        public static void CreateAppsettings()
        {
            string folderPath = "C:\\ProgramData\\Hunt\\HSAA_Projektarbeit\\src\\be-hunt-api";
            string appsettingsHuntsContent = @"
                {
                    ""ConnectionStrings"": {
                        ""HuntsDbConnection"": ""Server=localhost;Port=3366;Database=desired_database_name_here;User=root;Password=desired_password_here""
                    },
                    ""RabbitMQHost"": ""localhost"",
                    ""RabbitMQPort"":  5672,
                    ""Logging"": {
                        ""LogLevel"": {
                            ""Default"": ""Information"",
                            ""Microsoft"": ""Warning"",
                            ""Microsoft.Hosting.Lifetime"": ""Information""
                        }
                    },
                    ""AllowedHosts"": ""*""
                }";
            string appsettingsParticipationContent = @"
                {
                    ""ConnectionStrings"": {
                        ""ParticipantsDbConnection"": ""Server=localhost;Port=3399;Database=desired_database_name_here;User=root;Password=desired_password_here"",
                        ""ParticipantsCacheConnection"": ""localhost:6379""
                    },
                    ""RabbitMQHost"": ""localhost"",
                    ""RabbitMQPort"":  5672,
                    ""Jwt"": {
                        ""Key"": ""Xx69420-EncryptionIsGood-69420xX"",
                        ""Issuer"": ""YourIssuer"",
                        ""Audience"": ""YourAudience""
                    },
                    ""Logging"": {
                        ""LogLevel"": {
                            ""Default"": ""Information"",
                            ""Microsoft"": ""Warning"",
                            ""Microsoft.Hosting.Lifetime"": ""Information""
                        }
                    },
                    ""AllowedHosts"": ""*""
                }";
            CreateFile(folderPath + "\\Hunts.Api", "\\appsettings.json", appsettingsHuntsContent);
            CreateFile(folderPath + "\\Participants.Api", "\\appsettings.json", appsettingsParticipationContent);

        }

        /// <summary>
        /// Creates the ipconfig.json file with the local IP address.
        /// </summary>
        public static void CreateIpConfigs()
        {
            string folderPath = "C:\\ProgramData\\Hunt\\HSAA_Projektarbeit\\src\\be-hunt-api";
            string ip = GetLocalIPAdress();
            string ipConfigContent = $@"{{""Ip"": ""{ip}""}}";
            CreateFile(folderPath + "\\Hunts.Api", "\\ipconfig.json", ipConfigContent);
            CreateFile(folderPath + "\\Participants.Api", "\\ipconfig.json", ipConfigContent);
        }

        /// <summary>
        /// Creates the .env files for the backend and frontend projects.
        /// </summary>
        public static void CreateEnvFiles()
        {
            string hostIP = GetLocalIPAdress();

            string folderPath = "C:\\ProgramData\\Hunt\\HSAA_Projektarbeit\\src";
            string huntAPIContent = "HUNTS_DB_NAME=desired_database_name_here\r\n" + "HUNTS_DB_ROOT_PWD=desired_password_here\r\n" + "PARTICIPANTS_DB_NAME=desired_database_name_here\r\n" + "PARTICIPANTS_DB_ROOT_PWD=desired_password_here";
            string huntWebGameContent = "PUBLIC_API_URL=http://" + hostIP + ":5500/participants/api\r\nPUBLIC_HUNT_API_URL=http://" + hostIP + ":5500/hunts/api\r\nPUBLIC_PARTICIPANT_API_URL=http://" + hostIP + ":5500/participants/api";

            CreateFile(folderPath + "\\be-hunt-api", "\\.env", huntAPIContent);
            CreateFile(folderPath + "\\fe-hunt-editor", "\\.env", "PUBLIC_API_URL=http://" + hostIP + ":5500/hunts/api");
            CreateFile(folderPath + "\\fe-hunt-web-game", "\\.env", huntWebGameContent);

        }

        /// <summary>
        /// Kills a process by its ID.
        /// </summary>
        /// <param name="processId">The ID of the process to kill.</param>
        public static void KillProcessById(int processId)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = $"/PID {processId} /T /F", // "/T" kills child processes, "/F" forces termination
                    UseShellExecute = false,
                    RedirectStandardOutput = true, 
                    RedirectStandardError = true, 
                    Verb = "runas"  // Run as Administrator
                };

                using (Process process = Process.Start(processStartInfo))
                {
                    // Read the standard output and error streams
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine($"Process {processId} terminated successfully.");
                        Console.WriteLine("Output: " + output);
                    }
                    else
                    {
                        Console.WriteLine($"Error terminating process {processId}:");
                        Console.WriteLine("Error: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error using taskkill: {ex.Message}");
            }
        }

        /// <summary>
        /// Stops the docker-compose process.
        /// </summary>
        public static void StopDockerCompose()
        {
            try
            {
                string programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                
                // Create a new process to run the docker-compose command
                var process = new Process();
                process.StartInfo.FileName = "docker-compose";
                process.StartInfo.Arguments = "stop"; //down
                process.StartInfo.UseShellExecute = false;  // We need to redirect output and error
                process.StartInfo.CreateNoWindow = true;  // Hide the command window
                process.StartInfo.WorkingDirectory = Path.Combine(programDataPath, "Hunt", "HSAA_Projektarbeit", "src", "be-hunt-api");
                process.Start();

                //Write Error to Logfile
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (!string.IsNullOrEmpty(error))
                {
                    Logfile.WriteLog($"Docker Compose Error: {error}");
                }

                //Logfile.WriteLog("Docker Compose down completed successfully.");
                Debug.WriteLine("Docker Compose down completed successfully.");
            }
            catch (Exception ex)
            {
                Logfile.WriteLog($"Error executing docker-compose down: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a file with the specified content at the given path.
        /// </summary>
        /// <param name="folderPath">The folder path where the file will be created.</param>
        /// <param name="file">The name of the file to create.</param>
        /// <param name="content">The content to write to the file.</param>
        public static void CreateFile(string folderPath,string file, string content)
        {
            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // Check if the file already exists
                if (!System.IO.File.Exists(folderPath + file))
                {
                    // Create the file and write content to it
                    System.IO.File.WriteAllText(folderPath + file, content);
                    Console.WriteLine($"File successfully created at {folderPath + file}");
                }
                Console.WriteLine($"File successfully created at {folderPath + file}");
            }
            catch (Exception ex)
            {
                Logfile.WriteLog($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the local IP address of the machine.
        /// </summary>
        /// <returns>The local IP address as a string.</returns>
        public static string GetLocalIPAdress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return null;
            }
            catch (Exception)
            {
                Logfile.WriteLog("No network adapters with an IPv4 address in the system!");
                return null;
            }
        }

        /// <summary>
        /// Checks if a process with the specified name and command is running.
        /// </summary>
        /// <param name="processName">The name of the process to check.</param>
        /// <param name="command">The command line argument to check for.</param>
        /// <returns>True if the process is running, otherwise false.</returns>
        private static bool IsProcessRunning(string processName, string command)
        {
            Process[] processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                try
                {
                    if (process.ProcessName.Equals(processName, StringComparison.OrdinalIgnoreCase))
                    {
                        string commandLine = GetCommandLineArguments(process.Id);

                        // Check if the command line contains "docker-compose up --build"
                        if (command == null || (commandLine != null && commandLine.Contains(command)))
                        {
                            Debug.WriteLine(process.Id);
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logfile.WriteLog($"Error: {ex.Message}");
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the command line arguments of a process by its ID.
        /// </summary>
        /// <param name="processId">The ID of the process.</param>
        /// <returns>The command line arguments as a string.</returns>
        private static string GetCommandLineArguments(int processId)
        {
            try
            {
                // Use the WMI (Windows Management Instrumentation) to get the command line of the process
                var processQuery = new SelectQuery($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {processId}");
                var searcher = new ManagementObjectSearcher(processQuery);
                var queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    return m["CommandLine"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                Logfile.WriteLog($"Error: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Gets the installation path of Docker Desktop from the registry.
        /// </summary>
        /// <returns>The path to Docker Desktop executable.</returns>
        private static string GetDockerDesktopPath()
        {
            const string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Docker Desktop";
            const string valueName = "DisplayIcon";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                if (key != null)
                {
                    return key.GetValue(valueName) as string;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the Docker Desktop executable path by searching common installation directories.
        /// </summary>
        /// <returns>The path to Docker Desktop executable.</returns>
        private static string FindDockerDesktopPath()
        {
            // Explicitly handle 64-bit and 32-bit Program Files directories
            string programFiles64 = Environment.GetEnvironmentVariable("ProgramW6432");
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            string[] searchPaths = {
            Path.Combine(programFiles64, "Docker", @"Docker\Docker Desktop.exe"),
            Path.Combine(programFilesX86, "Docker", @"Docker\Docker Desktop.exe"),
        };

            foreach (string path in searchPaths)
            {
                if (System.IO.File.Exists(path))
                {
                    // Return the first found path
                    return path;
                }
            }

            return null;
        }
    }
}
