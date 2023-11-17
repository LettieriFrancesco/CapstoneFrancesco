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
    public class ScaffaleMagazzinoController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: ScaffaleMagazzino
        public ActionResult Index()
        {
            var scaffale_Magazzino = db.Scaffale_Magazzino.Include(s => s.Corsie_Magazzino);
            return View(scaffale_Magazzino.ToList());
        }

        // GET: ScaffaleMagazzino/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scaffale_Magazzino scaffale_Magazzino = db.Scaffale_Magazzino.Find(id);
            if (scaffale_Magazzino == null)
            {
                return HttpNotFound();
            }
            return View(scaffale_Magazzino);
        }

        // GET: ScaffaleMagazzino/Create
        public ActionResult Create()
        {
            ViewBag.IdCorsia = new SelectList(db.Corsie_Magazzino, "IdCorsia", "DescrizioneCorsia");
            return View();
        }

        // POST: ScaffaleMagazzino/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdScaffale,DescrizioneScaffale,IdCorsia")] Scaffale_Magazzino scaffale_Magazzino)
        {
            if (ModelState.IsValid)
            {
                db.Scaffale_Magazzino.Add(scaffale_Magazzino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCorsia = new SelectList(db.Corsie_Magazzino, "IdCorsia", "DescrizioneCorsia", scaffale_Magazzino.IdCorsia);
            return View(scaffale_Magazzino);
        }

        // GET: ScaffaleMagazzino/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scaffale_Magazzino scaffale_Magazzino = db.Scaffale_Magazzino.Find(id);
            if (scaffale_Magazzino == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCorsia = new SelectList(db.Corsie_Magazzino, "IdCorsia", "DescrizioneCorsia", scaffale_Magazzino.IdCorsia);
            return View(scaffale_Magazzino);
        }

        // POST: ScaffaleMagazzino/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdScaffale,DescrizioneScaffale,IdCorsia")] Scaffale_Magazzino scaffale_Magazzino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scaffale_Magazzino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCorsia = new SelectList(db.Corsie_Magazzino, "IdCorsia", "DescrizioneCorsia", scaffale_Magazzino.IdCorsia);
            return View(scaffale_Magazzino);
        }

        // GET: ScaffaleMagazzino/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scaffale_Magazzino scaffale_Magazzino = db.Scaffale_Magazzino.Find(id);
            if (scaffale_Magazzino == null)
            {
                return HttpNotFound();
            }
            return View(scaffale_Magazzino);
        }

        // POST: ScaffaleMagazzino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Scaffale_Magazzino scaffale_Magazzino = db.Scaffale_Magazzino.Find(id);
            bool prodottiCorrelati = db.Scaffale_Prodotti.Any(x => x.IdScaffale == id);
            if (prodottiCorrelati)
            {
                TempData["ErroreEliminazione"] = "Impossibile eliminare. Sono presenti prodotti all'interno del magazzino";
                return RedirectToAction("Index", "Admin");
            }
            db.Scaffale_Magazzino.Remove(scaffale_Magazzino);
            TempData["EliminaScaffale"] = "Scaffale eliminato dal database";
            db.SaveChanges();
            return RedirectToAction("Index","ScaffaleMagazzino");
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
