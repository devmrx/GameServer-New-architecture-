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

        static void Main(string[] args)
        {
            string command = "";
           

            Console.WriteLine("===== Консоль управления игровым сервером =====" + Environment.NewLine);
            Console.WriteLine(ServerWork());
            Console.WriteLine("Введите 'help' чтобы посмотреть доступные команды, или 'exit' для выхода");

            while ((command = Console.ReadLine()) != "exit")
            {
                switch (command)
                {
                    case "help":
                        HelpComand();
                        break;
                    case "start":

                        break;
                    case "stop":

                        break;
                    case "users":


                        break;
                    case "games":

                        break;




                    

                }



            }










            Console.ReadKey();
        }

        public static string ServerWork()
        {
            return server.ServerWork ? "Сервер включен" : "Серрвер выключен";
        }

        public static void HelpComand()
        {
            Console.WriteLine("start - включение сервера");
            Console.WriteLine("stop  - выключение сервера");
            Console.WriteLine("users - вывод списка всех игроков на сервере");
            Console.WriteLine("games - вывод списка всех установленных игр на сервере");
            //Console.WriteLine("start - включение сервера");
            //Console.WriteLine("start - включение сервера");





        }

    }
}
