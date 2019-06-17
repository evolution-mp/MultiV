using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using MultiVShared;

namespace PlayMultiV
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ParseableVersion subprocessVersion = new ParseableVersion(0, 0, 0, 0);

            if (File.Exists("MultiV.dll"))
            {
                var versiontext =
                    System.Diagnostics.FileVersionInfo.GetVersionInfo("MultiV.dll").FileVersion.ToString();
                subprocessVersion = ParseableVersion.Parse(versiontext);
            }


            var playerSetings = new PlayerSettings();

            if (File.Exists("settings.xml"))
            {
                var ser = new XmlSerializer(typeof (PlayerSettings));
                using (var stream = File.OpenRead("settings.xml"))
                {
                    playerSetings = (PlayerSettings) ser.Deserialize(stream);
                }
            }
            else
            {
                var ser = new XmlSerializer(typeof(PlayerSettings));
                using (var stream = File.OpenWrite("settings.xml"))
                {
                    ser.Serialize(stream, playerSetings);
                }
            }

            try
            {
                using (var wc = new ImpatientWebClient())
                {
                    var internetTextVersion =
                        wc.DownloadString(playerSetings.MasterServerAddress.Trim('/') + "/launcherversion");
                    var internetVersion = ParseableVersion.Parse(internetTextVersion);

                    if (internetVersion > subprocessVersion)
                    {
                        wc.DownloadFile(playerSetings.MasterServerAddress.Trim('/') + "/launcher", "MultiV.dll");
                    }
                }
            }
            catch (WebException)
            {
            }

            IEnumerable<Type> validTypes;
            try
            {
                var ourAssembly = Assembly.LoadFrom("MultiV.dll");

                var types = ourAssembly.GetExportedTypes();
                validTypes = types.Where(t =>
                    !t.IsInterface &&
                    !t.IsAbstract)
                    .Where(t => typeof (ISubprocessBehaviour).IsAssignableFrom(t));
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e.Message, "CRITICAL ERROR");
                goto end;
            }


            if (!validTypes.Any())
            {
                MessageBox.Show("Failed to load assembly \"MultiV.dll\": no assignable classes found.",
                    "CRITICAL ERROR");
                goto end;
            }

            ISubprocessBehaviour mainBehaviour = null;
            foreach (var type in validTypes)
            {
                mainBehaviour = Activator.CreateInstance(type) as ISubprocessBehaviour;
                if (mainBehaviour != null)
                    break;
            }

            if (mainBehaviour == null)
            {
                MessageBox.Show("Failed to load assembly \"MultiV.dll\": assignable class is null.",
                    "CRITICAL ERROR");
                goto end;
            }

            mainBehaviour.Start();

            end:
            {
            }
        }
    }

    public interface ISubprocessBehaviour
    {
        void Start();
    }
}
