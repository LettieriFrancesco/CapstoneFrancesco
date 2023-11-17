using CapstoneProjectFrancesco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProjectFrancesco.Controllers
{
  
    public class HomeController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
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
        public ActionResult FiltriCategorie()
        {
            var categoria = db.Prodotti.Where(x=> x.IdProdotto == x.IdCategoria).ToList();
            return View(categoria);
        }
        public ActionResult ProdottiList()
        {
            var prodotti = db.Prodotti.ToList();
            return View(prodotti);
        }
        public ActionResult Vini()
        {
            return View(db.Prodotti.Where(x => x.Categorie.CategoriaProdotto == "Vini").ToList());
        }
        public ActionResult Olio()
        {
            return View(db.Prodotti.Where(x=> x.Categorie.CategoriaProdotto == "Olio d'oliva").ToList());
        }
        public ActionResult Salumi()
        {
            return View(db.Prodotti.Where(x => x.Categorie.CategoriaProdotto == "Salumi").ToList());
        }
        public ActionResult Pasta()
        {
            return View(db.Prodotti.Where(x => x.Categorie.CategoriaProdotto == "Pasta").ToList());
        }
        public ActionResult ChiSiamo()
        {
            return View();
        }
        [Authorize]
        public ActionResult MieiOrdini()
        {
            var utenteAutenticato = db.User.FirstOrDefault(u=> u.Email == User.Identity.Name);
                var ordiniUser = db.Dettaglio_Ordine.Where(o=> o.Ordine.IdUser == utenteAutenticato.IdUser).ToList();
            if(utenteAutenticato != null)
            {
                if(ordiniUser.Count > 0)
                {
                return View(ordiniUser);

                }
                else
                {
                TempData["OrdiniUser"] = "Non hai effettuato nessun ordine";

                }
            }
            return View(ordiniUser);
        }
        [HttpPost]
        //Creo la funzione carrello alla quale passo i dati dati relativi all'ordine del prodotto.
        public ActionResult Carrello(string quantita)
        {
            int idProdotto = Convert.ToInt16(Session["IdDetails"]);
            int Quantita = Convert.ToInt32(quantita);
            //int? userId = Session["UserId"] as int?;
            int userd = Convert.ToInt16(Session["UserId"]);
            Dettaglio_Ordine dettaglio = new Dettaglio_Ordine();
            dettaglio.IdProdotto = idProdotto;
            dettaglio.Quantita = Quantita;
            dettaglio.Prodotti = db.Prodotti.Find(idProdotto);
            List<Dettaglio_Ordine> listaDettaglio = Session["Carrello"] as List<Dettaglio_Ordine>;
            if (listaDettaglio == null)
            {
                listaDettaglio = new List<Dettaglio_Ordine>();
                listaDettaglio.Add(dettaglio);
                Session["Carrello"] = listaDettaglio;
                TempData["Aggiunto"] = "Prodotto aggiunto";
                return RedirectToAction("ViewCarrello", "Home");
            }
            foreach (Dettaglio_Ordine d in listaDettaglio)
            {
                if (d.IdProdotto == idProdotto)
                {
                    d.Quantita += Quantita;
                    Session["Carrello"] = listaDettaglio;
                    TempData["Aggiunto"] = "Prodotto aggiunto al carrello";
                    return RedirectToAction("ViewCarrello", "Home");
                }
            }
            listaDettaglio.Add(dettaglio);
            Session["Carrello"] = listaDettaglio;
            TempData["Aggiunto"] = "Prodotto aggiunto al carrello";
            return RedirectToAction("ViewCarrello", "Home");
        }
        [Authorize(Roles ="User")]
        // Da qui creo la mia view carrello e salvo i dettagli dell'ordine nella Session.
        public ActionResult ViewCarrello()
        {
            List<Dettaglio_Ordine> carrello = Session["Carrello"] as List<Dettaglio_Ordine>;
            if (carrello != null)
            {
                decimal totaleCarrello = 0;

                foreach (var item in carrello)
                {
                    decimal subtotale = item.Prodotti.Prezzo * item.Quantita;
                    totaleCarrello += subtotale;
                }

                ViewBag.TotaleCarrello = totaleCarrello;

                return View(carrello);
            }
            else
            {
                ViewBag.TotaleCarrello = 0;
                return View(new List<Dettaglio_Ordine>());
            }
            //int userId = Convert.ToInt32(Session["UserId"]);
            //return View(Session["Carrello"] as List<Dettaglio_Ordine>);
        }
        //Infine tramite il checkout aggiungo tutti i dati reperiti in precedenza e li salvo nel database (Tab. Ordine).
        public ActionResult AddOrdine(string Indirizzo)
        {
            //var user = db.User.FirstOrDefault(x => x.Indirizzo == Indirizzo);
            //int userId = Convert.ToInt32(Session["UserId"]);
            var user = User.Identity.Name;
            var u = db.User.Where(x=> x.Email == user).FirstOrDefault();
            var indirizzoOrdine = db.User.FirstOrDefault(x=> x.IdUser == u.IdUser).Indirizzo;
            decimal importo = 0;
            List<Dettaglio_Ordine> carrello = Session["Carrello"] as List<Dettaglio_Ordine>;
            foreach (var item in carrello)
            {
                var totale = item.Prodotti.Prezzo * item.Quantita;
                importo += totale;
            }
            Ordine ordine = new Ordine();
            ordine.Data = DateTime.Now;
            ordine.Importo = importo;
            ordine.StatoOrdine = "Ordine da evadere";
            ordine.StatoConsegna = "Ordine da elaborare";
            if(Indirizzo == "")
            {
                ordine.Indirizzo = indirizzoOrdine;
            }
            else
            {
                ordine.Indirizzo = Indirizzo;
            }
            //if (Indirizzo != "")
            //{
            //    ordine.Indirizzo = Indirizzo;
            //}
            //else
            //{
            //    ordine.Indirizzo = user.Indirizzo;
            //}
            ordine.IdUser = u.IdUser;
            db.Ordine.Add(ordine);
            foreach (var item in carrello)
            {
                item.IdOrdine = ordine.IdOrdine;
                item.Prodotti = null;
                db.Dettaglio_Ordine.Add(item);
            }
            db.SaveChanges();
            Session.Remove("Carrello");
            TempData["Concluso"] = "Il tuo ordine è andato a buon fine";
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "User")]
        public ActionResult EliminaProdotto(int id)
        {
            List<Dettaglio_Ordine> carrello = Session["Carrello"] as List<Dettaglio_Ordine>;

            if (carrello != null)
            {
                var prodottoDaRimuovere = carrello.FirstOrDefault(d => d.IdProdotto == id);

                if (prodottoDaRimuovere != null)
                {
                    carrello.Remove(prodottoDaRimuovere);
                    if(carrello.Count > 0)
                    {
                    // Aggiorna il carrello nella sessione
                    Session["Carrello"] = carrello;

                    TempData["Eliminato"] = "Prodotto rimosso dal carrello.";
                        //return View();

                    }
                    else
                    {
                        Session.Remove("Carrello");
                        //TempData["Eliminato"] = "Carrello vuoto";
                    }

                }
            }

            return RedirectToAction("ViewCarrello");
        }

        [Authorize(Roles = "User")]
        public ActionResult Sottrai(int id)
        {
            List<Dettaglio_Ordine> carrello = Session["Carrello"] as List<Dettaglio_Ordine>;
            Dettaglio_Ordine prodottoDaModificare = carrello.FirstOrDefault(d => d.IdProdotto == id);

            if (prodottoDaModificare != null)
            {
                if(prodottoDaModificare.Quantita > 1)
                {
                prodottoDaModificare.Quantita -= 1;
                Session["Carrello"] = carrello;
                }
                else
                {
                    carrello.Remove(prodottoDaModificare);
                    if(carrello.Count > 0)
                    {
                    Session["Carrello"] = carrello;
                    }
                    else
                    {
                        Session.Remove("Carrello");
                    }
                }
                return RedirectToAction("ViewCarrello","Home");
                
            }

            return HttpNotFound();
        }
        [Authorize(Roles = "User")]
        public ActionResult Aggiungi(int id)
        {
            List<Dettaglio_Ordine> carrello = Session["Carrello"] as List<Dettaglio_Ordine>;
            Dettaglio_Ordine prodottoDaModificare = carrello.FirstOrDefault(d => d.IdProdotto == id);

            if (prodottoDaModificare != null)
            {
                prodottoDaModificare.Quantita += 1;
                Session["Carrello"] = carrello;
                return RedirectToAction("ViewCarrello","Home");
            }

            return HttpNotFound();
        }
    }
}
