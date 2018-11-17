using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore.MLogic {
    public interface IServer<TGame, TAccount>
    {
        bool ServerWork { get; set; }

       

        void Start();
        void Stop();
        void SetMethodLog(Action<string> worker);
        void SetMethodQuestionOutput(Func<string, bool> method);
        List<TGame> GetAllGames();
        List<TAccount> GetAllAccounts();
    }
}
