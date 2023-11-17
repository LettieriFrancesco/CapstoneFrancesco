using CapstoneProjectFrancesco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace CapstoneProjectFrancesco.Controllers
{
    public class RicercaProdottiController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: RicercaProdotti
        public ActionResult RicercaProdotto()
        {
            ViewBag.IdProdotti = new SelectList(db.Prodotti, "IdProdotto", "NomeProdotto");
            return View();
        }
        [HttpPost]
        public JsonResult RicercaProdottoScaffale(int Idprodotti)
        {
           Scaffale_Prodotti scaffale = db.Scaffale_Prodotti.Where(x=> x.IdProdotto == Idprodotti).FirstOrDefault();
           Scaffale_Prodotti scaffale1 = new Scaffale_Prodotti();
            if(scaffale != null)
            {
                Scaffale_Magazzino IdCorsia = db.Scaffale_Magazzino.Where(x=> x.IdScaffale == scaffale.IdScaffale).FirstOrDefault();
                Corsie_Magazzino corsia = db.Corsie_Magazzino.Where(x=> x.IdCorsia == IdCorsia.IdCorsia).FirstOrDefault();
                Prodotti p = db.Prodotti.Where(x => x.IdProdotto == Idprodotti).FirstOrDefault();
                scaffale1 = new Scaffale_Prodotti { IdProdotto = scaffale.IdProdotto, IdScaffale = scaffale.IdScaffale, NomeScaffale = scaffale.Scaffale_Magazzino.DescrizioneScaffale, Corsia = corsia.DescrizioneCorsia, NomeProdottoScaffale = p.NomeProdotto };
            }
            return Json(scaffale1);
        }
        
    }
}