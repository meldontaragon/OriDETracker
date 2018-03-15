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
       public Logger(String AppName, String Version)
        {
            try
            {

                DateTime utcDate = DateTime.UtcNow;

                app_data_path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                log_file = "LogFile_" + utcDate.Date.ToShortDateString() + "_v" + Version + ".txt";
                if (!(Directory.Exists(app_data_path + @"\MeldonTaragon")))
                {
                    Directory.CreateDirectory(app_data_path + @"\MeldonTaragon");
                }
                if (!(Directory.Exists(app_data_path + @"\MeldonTaragon\" + AppName)))
                {
                    Directory.CreateDirectory(app_data_path + @"\MeldonTaragon\" + AppName);
                }

                full_path = app_data_path + @"\MeldonTaragon\" + AppName + @"\" + log_file;
            }
            catch (Exception exc)
            {
                  System.Windows.Forms.MessageBox.Show(exc.ToString(), "WTF!");
            }
        }

        public Logger(String AppName, System.Version Version) : this(AppName, Version.ToString())
        {
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
