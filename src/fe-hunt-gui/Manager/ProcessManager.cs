using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fe_hunt_gui.Manager
{
    public static class ProcessManager
    {
        public static Process CurrentCmdProcess { get; set; }
        public static int CurrentCmdID { get; set; }
        //public static int DockerID { get; set; }

        public static Process CurrentBEdockerProcess { get; set; }
        public static int BEdockerComposeID { get; set; }

    }
}
