using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Middleware {
    public static class Util {
        private static DirectoryInfo GetAppDirectory() {
            return new DirectoryInfo(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        }

        public static Configuration GetConfiguration() {
            try {
                var exePath = GetAppDirectory().FullName;
                var settingsPath = Path.Join(exePath, "config", "settings.json");

                var settingsContent = string.Empty;
                using (var fs = File.OpenRead(settingsPath))
                using (var sr = new StreamReader(fs, new UTF8Encoding()))
                    settingsContent = sr.ReadToEnd();

                return JsonConvert.DeserializeObject<Configuration>(settingsContent);
            }
            catch (Exception exc) {
                Console.WriteLine(exc.Demystify());
                throw;
            }
        }
    }
}