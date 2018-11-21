using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameServerData;
using GameServerData.Model;
using GameServerCore.MLogic.Games;


namespace GameServerCore.MLogic
{
    public class Server : IServer<GameServer, Account>, IDisposable
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string HostName { get; set; }
        public bool ServerWork { get; set; } = false;

        public IRepository<User> connectionDb;
        private List<GameServer> Games { get; set; }
        private List<Account> Accounts { get; set; }

        private Queue<Account> QueueActiveAccount;

        private Action<string> WriteLog;
        public Func<string, bool> QuestionOutput;


        public void SetMethodLog(Action<string> method) {
            WriteLog += method;
        }

        public void SetMethodQuestionOutput(Func<string, bool> method) {
            QuestionOutput += method;
        }

        public Server() 
        {
            Ip = ServerConfig.Ip;
            Port = ServerConfig.Port;
            HostName = ServerConfig.HostName;

            QueueActiveAccount = new Queue<Account>();
            Mapper.Initialize(cfg => cfg.CreateMap<User, Gamer>());
        }

        public List<GameServer> GetAllGames() => Games;
        public List<Account> GetAllAccounts() => Accounts;

        public void SaveLog(string action)
        {
            if (WriteLog != null && ServerConfig.LogActivate)
            {
                WriteLog(action);
            }
        }

        public bool LogWriterOnOff
        {
            set => ServerConfig.LogActivate = value;
            get => ServerConfig.LogActivate;
        }


        private void SelectActiveAccounts()
        {
            SaveLog("Добавление игроков со статусом онлайн в очередь");

            if (Accounts == null)
            {
                SaveLog("Ошибка! колекция игроков пуста!");
                return;
            }

            foreach (Gamer account in Accounts)
            {
                if(account.GamerStatus == GameServerCore.MLogic.Status.Online)
                    QueueActiveAccount.Enqueue(account);
            }

            
        }


        private void SelectGame()
        {
            SaveLog("Распределение игроков в очереди по играм");

            if (QueueActiveAccount.Count != 0)
            {
                while (QueueActiveAccount.Count != 0 && Games.Count != 0) {
                    Account gamer = QueueActiveAccount.Dequeue();
                    gamer.Play();
                    Games[ServerEmulator.GetRandomIndxGame(Games.Count)].AddGamer(gamer);
                }

                foreach (var game in Games) {
                    game.GetCountPlayersFormat();
                }
                Games.Sort();
            }         
        }



        private void LoadGames() {

            SaveLog("Загрузка игр");

            Games = new List<GameServer>
            {
                new Chess(),
                new WOW(),
                new CSGO(),
                new PUBG(),
                new Dota2(),
                new Overwatch()
            };     
        }

        private void LoadAccounts() {
            // DB
            SaveLog("Подключение к базе данных");

            connectionDb = new UserDAL();
            connectionDb.OpenConnection();
           
            SaveLog("Загрузка пользователей");
            Accounts = Mapper.Map<List<User>, List<Gamer>>(connectionDb.GetAllList()).Cast<Account>().ToList();

            //Accounts = new List<Account>
            //{
            //    new Gamer(0, "Milena", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(1, "Dima", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(2, "darkness", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(3, "Viktor", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(4, "IronMan", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(5, "Rabbit", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(6, "Samurai", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(7, "Fox", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(8, "Dog12", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(9, "Stark", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(10, "Antoni", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(11, "Halk", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(12, "Marshmallow", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(13, "Nagibator228", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(14, "volk", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(15, "zoro", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(16, "stalker", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(17, "Rock123", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(18, "Linda", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(19, "Sniper", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(20, "Viper", ServerEmulator.GetRandomPassHash()),
            //    new Gamer(21, "mistic", ServerEmulator.GetRandomPassHash()),
            //};


        }

        

        public void Start()
        {
            SaveLog("Запуск игрового сервера");

            if (!ServerWork) {
                ServerWork = true;

                if (File.Exists("sessions.dat") 
                    && QuestionOutput != null 
                    && QuestionOutput("Найдена сохраненная сессия!\r\n Хотите её восстановить?"))
                {
                    DeserializeSessionsGames();

                    return;
                }


                // Load games and players
                // Start
                LoadGames();
                LoadAccounts();

                ServerEmulator.GetRandomStatus(Accounts);

                SelectActiveAccounts();
                SelectGame();

                StartGame();
            }
        }

        public void SaveSettings()
        {
            SaveLog("Сохранение настроек сервера");

            ServerConfig.SaveSettings();
        }

        private void StartGame()
        {
            foreach (var game in Games)
            {
                game.CreateSessions();
            }
        }

        //public void Restart();

        public void Stop() {

            SaveLog("Выключение игрового сервера");

            if (QuestionOutput != null
                && QuestionOutput("Сохранить текущюю сессию?"))
            {
                SaveLog("Сохранение сессии");
                SerializeSessionsGames();
            }
            else if(File.Exists("sessions.dat"))
            {
                File.Delete("sessions.dat");
            }

            Dispose();

            ServerWork = false;
        }

      
        // TODO: Доработать метоы сериализации

        private void SerializeSessionsGames()
        {
            SaveLog("Сохранение сессии");

            BinaryFormatter formatter = new BinaryFormatter();
            

            using (FileStream fs = new FileStream("sessions.dat", FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, Games);

                //Debug.WriteLine("Объект сериализован");
            }

        }

        private void DeserializeSessionsGames()
        {
            SaveLog("Востановление сессии");

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("sessions.dat", FileMode.OpenOrCreate)) {
                Games = (List<GameServer>)formatter.Deserialize(fs);

                //QueueActiveAccount = Games.Select(game => game._listGamers.Select(gamer => gamer)).AsQueryable<Account>();

                foreach (var game in Games)
                {
                    foreach (var gamer in game._listGamers)
                    {
                        QueueActiveAccount.Enqueue(gamer);
                    }
                }

                LoadAccounts();

                foreach (var account in Accounts)
                {
                    foreach (var accountActive in QueueActiveAccount)
                    {
                        if (account.Id == accountActive.Id)
                        {
                            ((Gamer)account).GamerStatus = GameServerCore.MLogic.Status.Online;
                            break;
                        }
                    }
                }
                //Debug.WriteLine("Объект десериализован");
            }
        }

        public void BannedUser(int id)
        {
            Account gamer = Accounts.First(s => s.Id == id);

            (connectionDb as UserDAL)?.UpdateBanned(id, !gamer.IsBanned);
            gamer.IsBanned = !gamer.IsBanned;
            gamer.Banned();
            
        }

        

        public void Dispose()
        {
            Accounts = null;
            Games = null;

            connectionDb.CloseConnection();
        }
    }
}
