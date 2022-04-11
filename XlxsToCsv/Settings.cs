using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XlsxToCsv
{
    internal class Settings
    {
        [JsonIgnore]
        public static Settings StaticSettings {
            get
            {
                if (loadedSettings is null)
                {
                    try
                    {
                        string json = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                         "\\XlsxToCsv\\settings.json");
                        loadedSettings = JsonConvert.DeserializeObject<Settings>(json) ?? new Settings();
                    }
                    catch
                    {
                        loadedSettings = new Settings();
                    }
                }



                return loadedSettings;

            }
        }

        public static void SaveSettings()
        {
            if (loadedSettings is not null)
            {
                try
                {

                    string json = JsonConvert.SerializeObject(loadedSettings);

                    Directory.CreateDirectory(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                        "\\XlsxToCsv");
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                      "\\XlsxToCsv\\settings.json", json);

                }
                catch
                {
                    // ignored
                }
            }
        }

        [JsonIgnore] private static Settings? loadedSettings = null;

        public string lastPath = "";
        public bool copyResult = false;



    }
}
