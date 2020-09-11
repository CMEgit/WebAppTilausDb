using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDb.Models;


namespace WebAppTilausDb.Controllers
{
    public class HomeController : Controller
    {
        private TilausDb3Entities db = new TilausDb3Entities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Vieras";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            return View();
        }
        public ActionResult Login() //kopioitu kurssimateriaaleista
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            TilausDb3Entities db = new TilausDb3Entities();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successful login";
                ViewBag.LoggedStatus = "Kirjautunut";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessful";
                ViewBag.LoggedStatus = "Vieras";
                LoginModel.LoginErrorMessage = "Virheellinen käyttäjätunnus tai salasana.";
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Vieras";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }

        //en tajua mitä logoutin kanssa tulis tehdä
    }
}