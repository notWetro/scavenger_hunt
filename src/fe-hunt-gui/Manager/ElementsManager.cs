using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace fe_hunt_gui.Manager
{
    public static class ElementsManager
    {
        public static Button Exit_Button { get; set; }

        public static ColumnDefinition EditorColumn { get; set; }
        public static Button Editor_Button { get; set; }

        public static ColumnDefinition ParticipationColumn { get; set; }
        public static Button Participation_Button { get; set; }

        public static ColumnDefinition PlayColumn { get; set; }
        public static Button Play_Hunt_Button { get; set; }

        public static Titlebar Titlebar { get; set; }
        public static LoadingCircle LoadingCircle { get; set; }

        public static StartingDocker StartDocker { get; set; }

        public static void DisableAllButtons()
        {
            Editor_Button.IsEnabled = false;
            Participation_Button.IsEnabled = false;
            Play_Hunt_Button.IsEnabled = false;
        }

        public static void EnableAllButtons()
        {
            Editor_Button.IsEnabled = true;
            Participation_Button.IsEnabled = true;
            Play_Hunt_Button.IsEnabled = true;
        }

    }
}
