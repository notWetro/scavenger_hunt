using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace fe_hunt_gui.Manager
{
    /// <summary>
    /// Manages UI elements within the application.
    /// </summary>
    public static class ElementsManager
    {
        public static Button Exit_Button { get; set; }
        public static ColumnDefinition EditorColumn { get; set; }
        public static Button Editor_Button { get; set; }
        public static ColumnDefinition PlayColumn { get; set; }
        public static Button Play_Button { get; set; }
        public static Titlebar Titlebar { get; set; }
        public static LoadingCircle LoadingCircle { get; set; }
        public static StartingDocker StartDocker { get; set; }

        /// <summary>
        /// Disables all buttons in the UI.
        /// </summary>
        public static void DisableAllButtons()
        {
            Editor_Button.IsEnabled = false;
            Play_Button.IsEnabled = false;
        }

        /// <summary>
        /// Enables all buttons in the UI.
        /// </summary>
        public static void EnableAllButtons()
        {
            Editor_Button.IsEnabled = true;
            Play_Button.IsEnabled = true;
        }
    }
}
