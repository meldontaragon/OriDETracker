using System;
using System.Windows.Forms;

namespace OriDETracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Logger start_log = new Logger("MAIN-OriDETracker-v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            //start_log.WriteToLog("**DEBUG** : In Main(), Starting Program");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //start_log.WriteToLog("**DEBUG** : About to start actual Tracker (see core log files for more info");
            Application.Run(new Tracker());
        }
    }
}