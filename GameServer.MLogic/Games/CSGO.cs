using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic.Games
{
    [Serializable]
    class CSGO : GameServer {


        public CSGO() {
            Name = "Counter-Strike: Global Offensive";
            ShortName = "Counter-Strike: Gl...";
            ImgGame = "Image/CounterStrikeGO.jpg";
            MaxGamersSession = 10;
            CountGamers = ServerEmulator.GetRandomCountGemers();
            //CountGamersF = GetCountPlayersFormat(CountGamers);
        }
    }
}
