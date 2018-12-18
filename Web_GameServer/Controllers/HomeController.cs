using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameServerCore.MLogic;
using GameServerCore.MLogic.Games;

namespace Web_GameServer.Controllers {
    public class HomeController : Controller
    {
        private IServer<GameServer, Account> server = ServerPath.Server;

        public ActionResult Index()
        {
            ViewBag.IsWork = server.ServerWork;
            ViewBag.HostName = server.HostName;
            ViewBag.IP = server.Ip;

            ViewBag.CountGamers = server.GetAllAccounts()?.Count;
            ViewBag.CountGames = server.GetAllGames()?.Count;

            int countGameSessions = 0;

            if (server.GetAllGames() != null)
            {
                foreach (var game in server.GetAllGames()) {
                    countGameSessions += game.GameSessions.Count;
                }
            }

            ViewBag.CountGameSessions = countGameSessions;

            return View();
        }


        public JsonResult JsonStartServer() {
            //var jsondata = db.Books.Where(a => a.Author.Contains(name)).ToList<Book>();

            server.Start();

            string IsWork = "Сервер: " + (server.ServerWork ? "включен" : "выключен");

            string CountGamers = "Количество игроков на сервере: " + server.GetAllAccounts().Count.ToString();
            string CountGames = "Количество установленных игр на сервере: " + server.GetAllGames().Count.ToString();

            int countSessions = 0;

            List<int> raspr = SetCountGamers();
            foreach (var game in server.GetAllGames()) {
                countSessions += game.GameSessions.Count;
            }

            string CountGameSessions = "Количество игровых сессий в текущий момент: " + countSessions;


            return Json(new { IsWork, CountGamers, CountGames, CountGameSessions, raspr }, JsonRequestBehavior.AllowGet);
        }

        private List<int> SetCountGamers() {
            List<int> raspr = new List<int>();

            raspr.Add(server.GetAllGames().FirstOrDefault(g => g.Id == 0)._listGamers.Count);   // Chess
            raspr.Add(server.GetAllGames().FirstOrDefault(g => g.Id == 1)._listGamers.Count);  // Csgo
            raspr.Add(server.GetAllGames().FirstOrDefault(g => g.Id == 2)._listGamers.Count); // Dota2
            raspr.Add(server.GetAllGames().FirstOrDefault(g => g.Id == 3)._listGamers.Count);  // Overwatch
            raspr.Add(server.GetAllGames().FirstOrDefault(g => g.Id == 4)._listGamers.Count);  //  Pubg
            raspr.Add(server.GetAllGames().FirstOrDefault(g => g.Id == 5)._listGamers.Count);  // Wow

            return raspr;
        }

        public JsonResult JsonStopServer()
        {
            server.Stop();

            string IsWork = "Сервер: " + (server.ServerWork ? "включен" : "выключен");

            return Json(new { IsWork }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Setting() {
            ViewBag.Message = "Setting";

            return PartialView();
        }

        public ActionResult Users(int? id) {

            if (id != null)
            {
                server.BannedUser((int)id);        
            }
            
            return PartialView(server.GetAllAccounts());
        }

        //public ActionResult UpdateBan(int id)
        //{
        //    server.BannedUser(id);

        //    return PartialView();
        //}


        public ActionResult Games()
        {

            return PartialView(server.GetAllGames());
        }

    }
}