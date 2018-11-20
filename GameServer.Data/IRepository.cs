using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerData {
    public interface IRepository<T>
    {
        void OpenConnection();
        void OpenConnection(string connectionstring);
        void CloseConnection();
        List<T> GetAllList();
        DataTable GetAllDataTable();
        void Insert(T item);
        void Delete(int id);

    }
}
