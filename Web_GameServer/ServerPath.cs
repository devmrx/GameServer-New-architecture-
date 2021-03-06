﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServerCore.MLogic;
using GameServerCore.MLogic.Games;

namespace Web_GameServer {
    public static class ServerPath {

        public static IServer<GameServer, Account> Server { get; private set; }

        public static void RegisterServer(IServer<GameServer, Account> server)
        {
            Server = server;
        }
    }
}