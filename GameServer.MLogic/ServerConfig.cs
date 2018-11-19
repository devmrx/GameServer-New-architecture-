using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameServerCore.MLogic {
     static class ServerConfig
    {
        public static string Ip { get; private set; } = "127.0.0.1";
        public static string HostName { get; private set; } = "gameserver.com";
        public static bool LogActivate { get; set; } = true;
        public static string Port = ConfigurationManager.AppSettings["port"].ToString();

        static ServerConfig()
        {
            GetSettingXml();
        }

        private static void GetSettingXml() {
            //XDocument xdoc = XDocument.Load("App.config");


        }

    }



    // Ограничить кол-во пользов., сессий
}
