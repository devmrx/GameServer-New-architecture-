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

        public ActionResult Index() {

            server.Start();

            ViewBag.IsWork = server.ServerWork;
            ViewBag.HostName = server.HostName;
            ViewBag.IP = server.Ip;

            ViewBag.CountGamers = server.GetAllAccounts().Count;
            ViewBag.CountGames = server.GetAllGames().Count;

            int countGameSessions = 0;

            foreach (var game in server.GetAllGames()) {
                countGameSessions += game.GameSessions.Count;
            }

            ViewBag.CountGameSessions = countGameSessions;

            return View();
        }

        //public ActionResult ServerStart() {
            


        //    return PartialView();
        //}

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Users() {


        //    return View(server.GetAllAccounts());
        //}


        public ActionResult Users(int? id) {

            if (id != null)
            {
                server.BannedUser((int)id);
                
            }
            
            return View(server.GetAllAccounts());
        }

        //public ActionResult UpdateBan(int id)
        //{
        //    server.BannedUser(id);

        //    return PartialView();
        //}


        public ActionResult Games()
        {

            return View(server.GetAllGames());
        }


    }
}