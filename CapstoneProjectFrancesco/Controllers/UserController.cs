using CapstoneProjectFrancesco.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProjectFrancesco.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        // GET: User
        public ActionResult Index()
        {
            return View(db.Prodotti.ToList());
        }
        public ActionResult Details(int? id)
        {
            Session["IdDetails"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }
        [HttpPost]
        public ActionResult Carrello(string quantita)
        {
            int idProdotto = Convert.ToInt16(Session["IdDetails"]);
            int Quantita = Convert.ToInt16(quantita);
            int? userId = Session["UserId"] as int?;
            Dettaglio_Ordine dettaglio = new Dettaglio_Ordine();
            dettaglio.IdProdotto = idProdotto;
            dettaglio.Quantita = Quantita;
            dettaglio.Prodotti = db.Prodotti.Find(idProdotto);
            List<Dettaglio_Ordine> listaDettaglio = Session["Carrello"] as List<Dettaglio_Ordine>;
            if(listaDettaglio == null)
            {
                listaDettaglio = new List<Dettaglio_Ordine>();
                listaDettaglio.Add(dettaglio);
                Session["Carrello"] = listaDettaglio;
                TempData["Aggiunto"] = "Prodotto aggiunto";
                return RedirectToAction("ViewCarrello", "User");
            }
            foreach (Dettaglio_Ordine d in listaDettaglio) 
            {
                if(d.IdProdotto == idProdotto)
                {
                    d.Quantita += Quantita;
                    Session["Carrello"] = listaDettaglio;
                    TempData["Aggiunto"] = "Prodotto aggiunto";
                    return RedirectToAction("ViewCarrello", "User");
                }
            }
            listaDettaglio.Add(dettaglio);
            Session["Carrello"] = listaDettaglio;
            TempData["Aggiunto"] = "Prodotto aggiunto";
            return RedirectToAction("ViewCarrello", "User");
        }
        public ActionResult ViewCarrello()
        {
           return View(Session["Carrello"] as List<Dettaglio_Ordine>);
        }
        public ActionResult AddOrdine()
        {
            int userId =Convert.ToInt32( Session["UserId"]);
            decimal importo = 0;
            List<Dettaglio_Ordine> carrello = Session["Carrello"] as List<Dettaglio_Ordine>;
            foreach(var item in carrello)
            {
                var totale = item.Prodotti.Prezzo * item.Quantita;
                importo += totale;
            }
            Ordine ordine = new Ordine();
            ordine.Data = DateTime.Now;
            ordine.Importo = importo;
            ordine.IdUser = userId;
            db.Ordine.Add(ordine);
            foreach(var item in carrello)
            {
                item.IdOrdine = ordine.IdOrdine;
                item.Prodotti = null;
                db.Dettaglio_Ordine.Add(item);
            }
            db.SaveChanges();
            Session.Remove("Carrello");
            TempData["Concluso"] = "Grazie spendaccione";
            return RedirectToAction("Index", "User");
        }
    }
}