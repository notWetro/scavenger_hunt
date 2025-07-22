using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace fe_hunt_gui
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public void ApplyTheme(string theme)
        {
            // Clear any existing theme resources
            Resources.MergedDictionaries.Clear();

            // Load the specified theme
            var themeDict = new ResourceDictionary
            {
                Source = new Uri($"Backround/{theme}.xaml", UriKind.Relative)
            };
            Resources.MergedDictionaries.Add(themeDict);
        }

    }
}
