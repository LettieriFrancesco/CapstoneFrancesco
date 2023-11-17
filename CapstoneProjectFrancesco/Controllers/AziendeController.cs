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
    public class AziendeController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Lista aziende.
        public ActionResult Index()
        {
            return View(db.Aziende.ToList());
        }

        // GET: Dettagli aziende.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aziende aziende = db.Aziende.Find(id);
            if (aziende == null)
            {
                return HttpNotFound();
            }
            return View(aziende);
        }

        // GET: Create view aziende.
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Mando in post i dati per creare le aziende e salvare tutti i dati nel database.
        public ActionResult Create(Aziende aziende, HttpPostedFileBase FotoAzienda, HttpPostedFileBase LogoAzienda)
        {
            if (ModelState.IsValid)
            {
                if(FotoAzienda != null && FotoAzienda.ContentLength > 0)
                {
                    aziende.FotoAzienda = FotoAzienda.FileName;
                    string pathSave = Server.MapPath("~/Content/FileUpload/") + FotoAzienda.FileName;
                    FotoAzienda.SaveAs(pathSave);
                }
                else
                {
                    aziende.FotoAzienda = "LogoApp.png";
                }
                if (LogoAzienda != null && LogoAzienda.ContentLength > 0)
                {
                    aziende.LogoAzienda = LogoAzienda.FileName;
                    string pathSave = Server.MapPath("~/Content/FileUpload/") + LogoAzienda.FileName;
                    LogoAzienda.SaveAs(pathSave);
                }
                else
                {
                    aziende.LogoAzienda = "LogoApp.png";
                }
                db.Aziende.Add(aziende);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aziende);
        }

        // GET: Dati aziende per effettuare le modifiche.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aziende aziende = db.Aziende.Find(id);
            if (aziende == null)
            {
                return HttpNotFound();
            }
            return View(aziende);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Mando in post tutti i dati che ho modificato e li salvo nel database.
        public ActionResult Edit(Aziende aziende,HttpPostedFileBase FotoAzienda, HttpPostedFileBase LogoAzienda)
        {
            ModelDBContext db1 = new ModelDBContext();
            Aziende azienda = db.Aziende.Find(aziende.IdAzienda);
            if (ModelState.IsValid)
            {
                if(FotoAzienda != null && FotoAzienda.ContentLength > 0)
                {
                    aziende.FotoAzienda = FotoAzienda.FileName;
                    string pathToSave = Server.MapPath("~/Content/FileUpload/") + FotoAzienda.FileName;
                    FotoAzienda.SaveAs(pathToSave);
                }
                else
                {
                    aziende.FotoAzienda = azienda.FotoAzienda;
                }
                if(LogoAzienda != null && LogoAzienda.ContentLength > 0)
                {
                    aziende.LogoAzienda = LogoAzienda.FileName;
                    string pathToSave = Server.MapPath("~/Content/FileUpload/") + LogoAzienda.FileName;
                    LogoAzienda.SaveAs(pathToSave);
                }
                else
                {
                    aziende.LogoAzienda = azienda.LogoAzienda;
                }
                db1.Entry(aziende).State = EntityState.Modified;
                TempData["AziendaMoficicata"] = "Modifiche salvate con successo";
                db1.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aziende);
        }

        // GET: Aziende/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aziende aziende = db.Aziende.Find(id);
            if (aziende == null)
            {
                return HttpNotFound();
            }
            return View(aziende);
        }

        // POST: Aziende/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aziende aziende = db.Aziende.Find(id);
            bool prodottiCorrelati = db.Prodotti.Any(x => x.IdAzienda == id);
            if (prodottiCorrelati)
            {
                TempData["ErroreEliminazione"] = "Elimina prima il prodotto correlato a questa azienda";
                return RedirectToAction("Index", "Prodotti");
            }
            db.Aziende.Remove(aziende);
            TempData["EliminataAzienda"] = "Azienda eliminata dell'elenco";
            db.SaveChanges();
            return RedirectToAction("Index","Aziende");
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
