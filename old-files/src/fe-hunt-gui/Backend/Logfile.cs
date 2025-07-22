using System;
using System.IO;

class Logfile
{
    private static readonly string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Hunt\\Logfile\\logfile.txt";

    /// <summary>
    /// Writes a log entry to the log file with a timestamp.
    /// Ensures the directory exists before writing.
    /// </summary>
    /// <param name="message">The log message to write.</param>
    public static void WriteLog(string message)
    {
        try
        {
            // Ensure the directory exists
            string directory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Write log entry to file with timestamp
            using (StreamWriter writer = new StreamWriter(logFilePath, true)) // 'true' for append mode
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
                writer.WriteLine(logEntry);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write to log file: {ex.Message}");
        }
    }
}
