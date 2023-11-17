using CapstoneProjectFrancesco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapstoneProjectFrancesco.Controllers
{
    public class LoginController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password)
        {
            
            var user = db.User.FirstOrDefault(x=> x.Email == Email && x.Password == Password);
            if(user != null)
            {
                //Session["UserId"] = user.IdUser;
                FormsAuthentication.SetAuthCookie(Email, false);
                if(user.Ruolo == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Errore = "Utente non trovato";
            return View();
            //if(ModelState.IsValid)
            //{
            //    User user = db.User.Where(ut => ut.Email == utente.Email && ut.Password == utente.Password).FirstOrDefault();
            //    if (user != null && user.Ruolo == "Admin")
            //    {
            //        FormsAuthentication.SetAuthCookie(user.Email, false);
            //        return RedirectToAction("Index","Admin");
            //    }
            //    if (user != null && user.Ruolo == "User")
            //    {
            //        Session["UserId"] = user.IdUser;
            //        FormsAuthentication.SetAuthCookie(user.Email, false);
            //        return RedirectToAction("Index", "User");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Credenziali inserite non valide");
            //        return View();
            //    }
            //}
            //return View();

        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User u)
        {
            u.Ruolo = "User";
            if (ModelState.IsValid)
            {
                var email = db.User.Where(x=> x.Email == u.Email).FirstOrDefault();
                if(email != null)
                {
                    ViewBag.Email = "Email già presente nel database";
                    return View();
                }
                var user = new User();
                user.Email = u.Email;
                user.Password = u.Password;
                db.User.Add(u);
                db.SaveChanges();
                TempData["Successo"] = "Registrazione effettuata con successo";
                //FormsAuthentication.SetAuthCookie(u.Email, false);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Errore = "Errore durante l procedura";
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }
    }
}