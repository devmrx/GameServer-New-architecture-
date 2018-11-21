using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerCore.MLogic;
using GameServerCore.MLogic.Games;

namespace Console_GameServer {
    class Program {

        static IServer<GameServer, Account> server = new Server();

        static void Main(string[] args) {
            string command = "";
            server.SetMethodLog(LogWriter.GetInst().WriteFileL);
            server.SetMethodQuestionOutput(DialogQuestion);

            Console.WriteLine("========= Консоль управления игровым сервером =========" + Environment.NewLine);
            Console.WriteLine(ServerWork());
            Console.WriteLine("Введите 'help' чтобы посмотреть доступные команды, или 'exit' для выхода");

            while ((command = Console.ReadLine()) != "exit") {
                switch (command) {
                    case "help":
                        HelpComand();
                        break;
                    case "start":
                        server.Start();
                        Console.WriteLine(ServerWork());
                        break;
                    case "stop":
                        server.Stop();
                        Console.WriteLine(ServerWork());
                        break;
                    case "users":
                        if (server.ServerWork) {
                            ShowUsers();
                        } else {
                            Console.WriteLine("Ошибка сервер выключен!");
                        }
                        break;
                    case "games":
                        if (server.ServerWork) {
                            ShowGames();
                        } else {
                            Console.WriteLine("Ошибка сервер выключен!");
                        }
                        break;
                    case "status":
                        if (server.ServerWork) {
                            Status();
                        } else {
                            Console.WriteLine("Ошибка сервер выключен!");
                        }
                        break;
                    default:
                        Console.WriteLine("Неверная команда! Повторите ввод.");
                        break;
                }
            }
        }

        public static string ServerWork() {
            return server.ServerWork ? "Сервер включен" : "Сервер выключен";
        }

        public static void ShowUsers() {
            Console.WriteLine("Список всех пользователей на сервере");
            Console.WriteLine("Id   Login   Status");

            foreach (Gamer gamer in server.GetAllAccounts()) {
                Console.WriteLine($"{gamer.Id}  {gamer.Login}   {gamer.GamerStatus}");
            }
            Console.WriteLine("Всего игроков - " + server.GetAllAccounts().Count);
        }

        public static void ShowGames() {
            Console.WriteLine("Список всех установленных игр на сервере");
            Console.WriteLine("Id   Name");

            foreach (GameServer game in server.GetAllGames()) {
                Console.WriteLine($"{game.Id}  {game.Name}");
            }
        }


        public static void HelpComand() {
            Console.WriteLine("start  - включение сервера");
            Console.WriteLine("stop   - выключение сервера");
            Console.WriteLine("users  - вывод списка всех игроков на сервере");
            Console.WriteLine("games  - вывод списка всех установленных игр на сервере");
            Console.WriteLine("status - статус сервера");
            //Console.WriteLine("start - включение сервера");

        }


        public static bool DialogQuestion(string question)
        {
            Console.WriteLine(question);
            Console.WriteLine("Для подтверждения введите 'y', либо любой другой символ для отмены.");
            string res = Console.ReadLine();

            return res == "y";
        }

        public static void Status()
        {
            int countGameSessions = 0;

            foreach (var game in server.GetAllGames()) {
                countGameSessions += game.GameSessions.Count;
            }

            Console.WriteLine("--------------------- Статус сервера ---------------------");
            Console.WriteLine($"Сервер: {(server.ServerWork ? "включен" : "выключен")}");
            Console.WriteLine($"Домен сервера: {server.HostName}");
            Console.WriteLine($"IP-адрес сервера: {server.Ip}");
            Console.WriteLine($"Количество установленных игр на сервере: {server.GetAllGames().Count}");
            Console.WriteLine($"Количество игроков на сервере: {server.GetAllAccounts().Count}");
            Console.WriteLine($"Количество игровых сессий в текущий момент: {countGameSessions}");
        }
    }
}
