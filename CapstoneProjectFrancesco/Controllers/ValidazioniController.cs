using CapstoneProjectFrancesco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProjectFrancesco.Controllers
{
    public class ValidazioniController : Controller
    {
        private static ModelDBContext db = new ModelDBContext();
        // GET: Validazioni
       public ActionResult IsEmailValid(string email)
        {
            bool isValid = db.User.All(x => x.Email != email);
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IsPasswordValid(string password)
        {
            bool isValid = db.User.All(x=> x.Password != password);
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }
    }
}