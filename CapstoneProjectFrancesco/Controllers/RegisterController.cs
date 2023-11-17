using CapstoneProjectFrancesco.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProjectFrancesco.Controllers
{
    public class RegisterController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Exclude = "Ruolo")]User u)
        {
            u.Ruolo = "User";
            if(ModelState.IsValid)
            {
                db.User.Add(u);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}