using CapstoneProjectFrancesco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProjectFrancesco.Controllers
{
    public class SearchProductController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        // GET: SearchProduct
        public ActionResult CercaProdotti(string ricerca)
        {
            var prodotti = db.Prodotti.Where(p => p.NomeProdotto.Contains(ricerca)).ToList();
            return View(prodotti);
        }
    }
}