using System;
using System.Diagnostics;
using System.IO;

namespace MultiV
{
    public class LogManager
    {
        public static string LogDirectory = Main.MultiVInstallDir + "\\logs";

        public static void CreateLogDirectory()
        {
            if (!Directory.Exists(Main.MultiVInstallDir + "\\logs"))
                Directory.CreateDirectory(Main.MultiVInstallDir + "\\logs");
        }

        public static void SimpleLog(string filename, string text)
        {
            CreateLogDirectory();
            File.AppendAllText(LogDirectory + "\\" + filename + ".log", text + "\r\n");
        }

        public static void DebugLog(string text)
        {
            CreateLogDirectory();
            if (!Main.WriteDebugLog) return;
            try
            {
                //File.AppendAllText(LogDirectory + "\\debug.log", text + "\r\n");
                Debug.WriteLine(text);
            }
            catch (Exception) { }
        }

        public static void LogException(Exception ex, string source)
        {
            CreateLogDirectory();
            File.AppendAllText(LogDirectory + "\\error.log", ">> EXCEPTION OCCURED AT " + DateTime.Now + " FROM " +source + "\r\n" +  ex.ToString() + "\r\n\r\n");
        }
    }
}