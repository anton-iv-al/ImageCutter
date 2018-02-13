using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ImageCutter
{
    public static class ConfigurationHelper
    {
        public static void SaveConfiguration(Dictionary<string, string> configParams) {
            string serializedParams = new SerializerBuilder().Build().Serialize(configParams);

            try {
                File.WriteAllText(ConfigurationFilePath(), serializedParams);
            }
            catch (Exception) {
                return;
            }
        }

        public static Dictionary<string, string> LoadConfiguration() {
            try {
                string serializedParams = File.ReadAllText(ConfigurationFilePath());
                return new DeserializerBuilder().Build().Deserialize<Dictionary<string, string>>(serializedParams);
            }
            catch (Exception) {
                return new Dictionary<string, string>();
            }
        }

        private static string ConfigurationFilePath() {
            return "configuration.yaml";
        }
    }
}
