using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic.Games {
    [Serializable]
    public abstract class GameServer : IComparable<GameServer>{

        public int Id { get; set; }    
        public string Name { get; set; }
        public string ImgGame { get; set; }
        //public string TypeGame { get; set; } // ENUM 
        public string ShortName { get; set; }
        public int CountGamers { get; set; }
        public string CountGamersF { get; protected set; }
        protected byte MaxGamersSession { get;  set; } 
        protected string Description { get; set; }

        public List<Account> _listGamers;
        public List<GameSession> GameSessions { get; private set; }

        protected GameServer()
        {
            _listGamers = new List<Account>();
            
        }

        public int CompareTo(GameServer other)
        {
            if (CountGamers > other.CountGamers) return -1;
            else if (CountGamers < other.CountGamers) return 1;
            else return 0;
    
        }


        // .... maybe set virtual
        public void CreateSessions()
        {
            GameSession gameSession = new GameSession(MaxGamersSession, DateTime.Now.AddMinutes(15)); // Продолжительность игровой сессии

            if (GameSessions == null)
            {
                GameSessions = new List<GameSession>();
            }

            for (int i = 0; i < _listGamers.Count; i++)
            {
                if (gameSession.GamersPlay.Count <= gameSession.MaxCountGamers) {
                    gameSession.GamersPlay.Add((Gamer)_listGamers[i]);

                    if(i == (_listGamers.Count-1)) GameSessions.Add(gameSession);
                } else {
                    GameSessions.Add(gameSession);

                    gameSession = new GameSession(MaxGamersSession, new DateTime().AddMinutes(15));
                }
            }
        }

        public void GetCountPlayersFormat()
        {
            CountGamers = _listGamers.Count;
            int count = CountGamers;
            int lastdigit = Int32.Parse(count.ToString().Last().ToString());

            if (count > 10)
            {
                CountGamersF = count + " игроков онлайн";
                return;
            }

            switch (lastdigit)
            {
                case 1:
                    CountGamersF = count + " игрок онлайн";
                    break;
                case 2:
                case 3:
                case 4:
                    CountGamersF = count + " игрока онлайн";
                    break;
                default:
                    CountGamersF = count + " игроков онлайн";
                    break;
            }
        }

        public void AddGamer(Account account)
        {
            _listGamers.Add(account);
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual void StartGame()
        {

        }


    }
}
