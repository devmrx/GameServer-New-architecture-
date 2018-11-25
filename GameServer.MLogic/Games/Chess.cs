using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic.Games
{
    [Serializable]
    class Chess : GameServer
    {
        public Chess()
        {
            Id = 0;
            Name = "Chess";
            ShortName = "Chess";
            ImgGame = "Images/Chess.jpg";
            MaxGamersSession = 2;
            CountGamers = ServerEmulator.GetRandomCountGemers();
            //CountGamersF = GetCountPlayersFormat(CountGamers);
        }

        private void CreateSession() {

        }
    }
}
