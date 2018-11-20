using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GameServerCore.MLogic {
     static class ServerConfig
     {
        private static readonly string _settingsFile = "Settings.xml";
        public static string Ip { get; private set; } 
        public static string HostName { get; private set; } 
        public static bool LogActivate { get; set; } 
        public static string Port { get; set; } //ConfigurationManager.AppSettings["port"].ToString();

        static ServerConfig()
        {
            GetSettingXml();
        }

        private static void GetSettingXml() {

            if (File.Exists(_settingsFile))
            {
                    XDocument xdoc = XDocument.Load(_settingsFile);
                    XElement settings = xdoc.Element("Settings");

                    LogActivate = Boolean.Parse(settings.Element("LogActivate").Value);
                    Ip = settings.Element("Ip").Value;
                    HostName = settings.Element("HostName").Value;
                    
                    Port = settings.Element("Port").Value;  
            }
            else
            {
                SetDefaultSettings();
            }    
        }

         private static void SetDefaultSettings()
         {
            Ip = "127.0.0.1";
            HostName = "gameserver.com";
            LogActivate  = true;
            Port = "5432";

            SaveSettings();
         }


         public static void SaveSettings()
         {
             XDocument xdoc = new XDocument(new XElement("Settings",
                    new XElement("Ip", Ip),
                    new XElement("HostName", HostName),
                    new XElement("LogActivate", LogActivate),
                    new XElement("Port", Port))
                       
             );
             xdoc.Save(_settingsFile);
        }

        // public static void GetSettings() {
        //     ServerConfig settings = null;
        //     string filename = _settingsFile;

        //     //проверка наличия файла
        //     if (File.Exists(filename)) {
        //         using (FileStream fs = new FileStream(filename, FileMode.Open)) {
        //             XmlSerializer xser = new XmlSerializer(typeof(ServerConfig));
        //             settings = (ServerConfig)xser.Deserialize(fs);
        //             fs.Close();
        //         }
        //     } else {
        //         settings = new Settings();
        //     }

             
        // }


        //public void Save() {
     

        //    if (File.Exists(_settingsFile)) File.Delete(_settingsFile);

        //    using (FileStream fs = new FileStream(_settingsFile, FileMode.Create)) {
        //        XmlSerializer xser = new XmlSerializer(typeof(ServerConfig));
        //        xser.Serialize(fs, this);
        //        fs.Close();
        //    }
        //}

    }



    // Ограничить кол-во пользов. сессий
}
