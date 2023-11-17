using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneProjectFrancesco.Models;

namespace CapstoneProjectFrancesco.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ScaffaleProdottiController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: ScaffaleProdotti
        public ActionResult Index()
        {
            var scaffale_Prodotti = db.Scaffale_Prodotti.Include(s => s.Prodotti).Include(s => s.Scaffale_Magazzino);
            return View(scaffale_Prodotti.ToList());
        }

        // GET: ScaffaleProdotti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scaffale_Prodotti scaffale_Prodotti = db.Scaffale_Prodotti.Find(id);
            if (scaffale_Prodotti == null)
            {
                return HttpNotFound();
            }
            return View(scaffale_Prodotti);
        }

        // GET: ScaffaleProdotti/Create
        public ActionResult Create()
        {
            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "NomeProdotto");
            ViewBag.IdScaffale = new SelectList(db.Scaffale_Magazzino, "IdScaffale", "DescrizioneScaffale");
            return View();
        }

        // POST: ScaffaleProdotti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdScaffaleProdotti,IdScaffale,IdProdotto")] Scaffale_Prodotti scaffale_Prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Scaffale_Prodotti.Add(scaffale_Prodotti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "NomeProdotto", scaffale_Prodotti.IdProdotto);
            ViewBag.IdScaffale = new SelectList(db.Scaffale_Magazzino, "IdScaffale", "DescrizioneScaffale", scaffale_Prodotti.IdScaffale);
            return View(scaffale_Prodotti);
        }

        // GET: ScaffaleProdotti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scaffale_Prodotti scaffale_Prodotti = db.Scaffale_Prodotti.Find(id);
            if (scaffale_Prodotti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "NomeProdotto", scaffale_Prodotti.IdProdotto);
            ViewBag.IdScaffale = new SelectList(db.Scaffale_Magazzino, "IdScaffale", "DescrizioneScaffale", scaffale_Prodotti.IdScaffale);
            return View(scaffale_Prodotti);
        }

        // POST: ScaffaleProdotti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdScaffaleProdotti,IdScaffale,IdProdotto")] Scaffale_Prodotti scaffale_Prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scaffale_Prodotti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "NomeProdotto", scaffale_Prodotti.IdProdotto);
            ViewBag.IdScaffale = new SelectList(db.Scaffale_Magazzino, "IdScaffale", "DescrizioneScaffale", scaffale_Prodotti.IdScaffale);
            return View(scaffale_Prodotti);
        }

        // GET: ScaffaleProdotti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scaffale_Prodotti scaffale_Prodotti = db.Scaffale_Prodotti.Find(id);
            if (scaffale_Prodotti == null)
            {
                return HttpNotFound();
            }
            return View(scaffale_Prodotti);
        }

        // POST: ScaffaleProdotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Scaffale_Prodotti scaffale_Prodotti = db.Scaffale_Prodotti.Find(id);
            db.Scaffale_Prodotti.Remove(scaffale_Prodotti);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
