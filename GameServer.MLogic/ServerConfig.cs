using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic {
    static class ServerConfig
    {
        public static string Ip { get; private set; } = "127.0.0.1";
        public static string HostName { get; private set; } = "gameserver.com";
        public static bool LogActivate { get; set; } = true;


    }




    // Ограничить кол-во пользов., сессий
}
