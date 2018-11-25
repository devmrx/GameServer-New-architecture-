﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic.Games
{
    [Serializable]
    class CSGO : GameServer {


        public CSGO()
        {
            Id = 1;
            Name = "Counter-Strike: Global Offensive";
            ShortName = "Counter-Strike: Gl...";
            ImgGame = "Images/CounterStrikeGO.jpg";
            MaxGamersSession = 10;
            CountGamers = ServerEmulator.GetRandomCountGemers();
            //CountGamersF = GetCountPlayersFormat(CountGamers);
        }
    }
}
