using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerData.Model {
    public class User {

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsBanned { get; set; }
        public DateTime? LastGameSession { get; set; }
    }
}
