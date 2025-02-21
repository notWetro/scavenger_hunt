using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fe_hunt_gui.Manager
{
    /// <summary>
    /// Manages processes related to the application.
    /// </summary>
    public static class ProcessManager
    {
        /// <summary>
        /// Gets or sets the current command process.
        /// </summary>
        public static Process CurrentCmdProcess { get; set; }

        /// <summary>
        /// Gets or sets the ID of the current command process.
        /// </summary>
        public static int CurrentCmdID { get; set; }

        /// <summary>
        /// Gets or sets the current backend Docker process.
        /// </summary>
        public static Process CurrentBEdockerProcess { get; set; }

        /// <summary>
        /// Gets or sets the ID of the backend Docker Compose process.
        /// </summary>
        public static int BEdockerComposeID { get; set; }
    }
}
