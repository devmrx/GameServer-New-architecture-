using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic.Games
{
    [Serializable]
    class GameSession {

        public int MaxCountGamers { get; set; }
        public List<Gamer> GamersPlay; // id
        public DateTime _sessionTime;



        public GameSession(int maxCountGamers, DateTime sessionTime) {
            MaxCountGamers = maxCountGamers;
            GamersPlay = new List<Gamer>(maxCountGamers);
            _sessionTime = sessionTime;
        }



    }
}
