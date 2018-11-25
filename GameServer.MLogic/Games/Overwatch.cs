using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic.Games
{
    [Serializable]
    class Overwatch : GameServer
    {
        public Overwatch()
        {
            Id = 3;
            Name = "Overwatch";
            ShortName = "Overwatch";
            ImgGame = "Images/Overwatch.jpg";
            MaxGamersSession = 30;
            CountGamers = ServerEmulator.GetRandomCountGemers();
            //CountGamersF = GetCountPlayersFormat(CountGamers);
        }
    }
}
