using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic {
    public interface IServer<TGame, TGamer>
    {
        bool ServerWork { get; set; }
        bool LogWriterOnOff { get; set; }
        List<TGame> GetAllGames();
        List<TGamer> GetAllAccounts();

        void Start();
        void Stop();
        void SetMethodLog(Action<string> worker);
        void SetMethodQuestionOutput(Func<string, bool> method);
        void BannedUser(int id);
        
    }
}
