using System.IO;
using System.Xml.Serialization;

namespace iSlack
{
    public class iSlackConfig
    {
        public bool hasValidSlackSetting { get; set; }
        public string Username { get; set; }
        public string Channel { get; set; }
        public string URL { get; set; }
    }

    public class Configuration
    {
        public static iSlackConfig Load(string filePath)
        {
            var xs = new XmlSerializer(typeof(iSlackConfig));
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open))
                {
                    return (iSlackConfig)xs.Deserialize(fs);
                }
            }
            catch
            {
                return new iSlackConfig();
            }
        }

        public static void Save(string filePath, iSlackConfig config)
        {
            var xs = new XmlSerializer(typeof(iSlackConfig));
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                xs.Serialize(fs, config);
            }
        }
    }
}
