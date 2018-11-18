using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerData {
    public interface IRepository<T>
    {
        void OpenConnection();
        void CloseConnection();
        List<T> GetAllList();
        void OpenConnection(string connectionstring);

    }
}
