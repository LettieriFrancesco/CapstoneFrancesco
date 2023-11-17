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
    public class CorsieMagazzinoController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: CorsieMagazzino
        public ActionResult Index()
        {
            return View(db.Corsie_Magazzino.ToList());
        }

        // GET: CorsieMagazzino/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corsie_Magazzino corsie_Magazzino = db.Corsie_Magazzino.Find(id);
            if (corsie_Magazzino == null)
            {
                return HttpNotFound();
            }
            return View(corsie_Magazzino);
        }

        // GET: CorsieMagazzino/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CorsieMagazzino/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCorsia,DescrizioneCorsia")] Corsie_Magazzino corsie_Magazzino)
        {
            if (ModelState.IsValid)
            {
                db.Corsie_Magazzino.Add(corsie_Magazzino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(corsie_Magazzino);
        }

        // GET: CorsieMagazzino/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corsie_Magazzino corsie_Magazzino = db.Corsie_Magazzino.Find(id);
            if (corsie_Magazzino == null)
            {
                return HttpNotFound();
            }
            return View(corsie_Magazzino);
        }

        // POST: CorsieMagazzino/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCorsia,DescrizioneCorsia")] Corsie_Magazzino corsie_Magazzino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corsie_Magazzino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(corsie_Magazzino);
        }

        // GET: CorsieMagazzino/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corsie_Magazzino corsie_Magazzino = db.Corsie_Magazzino.Find(id);
            if (corsie_Magazzino == null)
            {
                return HttpNotFound();
            }
            return View(corsie_Magazzino);
        }

        // POST: CorsieMagazzino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Corsie_Magazzino corsie_Magazzino = db.Corsie_Magazzino.Find(id);
            db.Corsie_Magazzino.Remove(corsie_Magazzino);
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
