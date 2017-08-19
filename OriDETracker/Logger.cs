using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace OriDETracker
{
    public class Logger
    {
       public Logger(String AppName)
        {
            app_data_path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            log_file = "OutputLog.txt";
            if (!(Directory.Exists(app_data_path + @"David_Miller")))
            {
                Directory.CreateDirectory(app_data_path + @"David_Miller");
            }
            if (!(Directory.Exists(app_data_path + @"David_Miller\" + AppName)))
            {
                Directory.CreateDirectory(app_data_path + @"David_Miller\" + AppName);
            }

            full_path = app_data_path + @"David_Miller\" + AppName + @"\" + log_file;

        }
        private string app_data_path;
        private string full_path;
        private string log_file;

        public void WriteToLog(string line)
        {
            DateTime utcDate = DateTime.UtcNow;
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");

            string log_text = utcDate.ToString(culture) + " -- " + line + "\n";

            File.AppendAllText(full_path, log_text);
        }
    }
}
