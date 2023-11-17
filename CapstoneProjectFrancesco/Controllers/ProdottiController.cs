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
    public class ProdottiController : Controller
    {
        
        private ModelDBContext db = new ModelDBContext();
       
        public ActionResult Index()
        {
            var prodotti = db.Prodotti.Include(p => p.Aziende).Include(p => p.Categorie);
            return View(prodotti.ToList());
        }
        public ActionResult CercaProdottiAdmin(string ricerca)
        {
            var prodotti = db.Prodotti.Where(p => p.NomeProdotto.Contains(ricerca)).ToList();
            return PartialView(prodotti);
        }

        public ActionResult Details(int? id)
        {
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

        public ActionResult Create()
        {
            ViewBag.IdAzienda = new SelectList(db.Aziende, "IdAzienda", "NomeAzienda");
            ViewBag.IdCategoria = new SelectList(db.Categorie, "IdCategoria", "CategoriaProdotto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prodotti prodotti, HttpPostedFileBase FotoProdotto1, HttpPostedFileBase FotoProdotto2, HttpPostedFileBase FotoProdotto3)
        {
            if (ModelState.IsValid)
            {
                if(FotoProdotto1 != null && FotoProdotto1.ContentLength > 0)
                {
                    prodotti.FotoProdotto1 = FotoProdotto1.FileName;
                    string pathToSave = Server.MapPath("~/Content/FileUpload/") + FotoProdotto1.FileName;
                    FotoProdotto1.SaveAs(pathToSave);
                }
                else 
                {
                    prodotti.FotoProdotto1 = "LogoApp.png";
                }
                if (FotoProdotto2 != null && FotoProdotto2.ContentLength > 0)
                {
                    prodotti.FotoProdotto2 = FotoProdotto2.FileName;
                    string pathToSave = Server.MapPath("~/Content/FileUpload/") + FotoProdotto2.FileName;
                    FotoProdotto2.SaveAs(pathToSave);
                }
                else
                {
                    prodotti.FotoProdotto2 = "LogoApp.png";
                }
                if (FotoProdotto3 != null && FotoProdotto3.ContentLength > 0)
                {
                    prodotti.FotoProdotto3 = FotoProdotto3.FileName;
                    string pathToSave = Server.MapPath("~/Content/FileUpload/") + FotoProdotto3.FileName;
                    FotoProdotto3.SaveAs(pathToSave);
                }
                else
                {
                    prodotti.FotoProdotto3 = "LogoApp.png";
                }
                db.Prodotti.Add(prodotti);
                TempData["ProdottoAggiunto"] = "Prodotto aggiunto";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAzienda = new SelectList(db.Aziende, "IdAzienda", "NomeAzienda", prodotti.IdAzienda);
            ViewBag.IdCategoria = new SelectList(db.Categorie, "IdCategoria", "CategoriaProdotto", prodotti.IdCategoria);
            return View(prodotti);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAzienda = new SelectList(db.Aziende, "IdAzienda", "NomeAzienda", prodotti.IdAzienda);
            ViewBag.IdCategoria = new SelectList(db.Categorie, "IdCategoria", "CategoriaProdotto", prodotti.IdCategoria);
            return View(prodotti);
        }

        // POST: Prodotti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prodotti prodotti, HttpPostedFileBase FotoProdotto1, HttpPostedFileBase FotoProdotto2, HttpPostedFileBase FotoProdotto3)
        {
            ModelDBContext db1 = new ModelDBContext();
            //int id = Convert.ToInt32(TempData["IdProdotto"]);
            Prodotti prodotto = db.Prodotti.Find(prodotti.IdProdotto);
            //Prodotti prodotto =db.Prodotti.FirstOrDefault(x=> x.IdProdotto == id);
            if (ModelState.IsValid)
            {
                if (FotoProdotto1 != null && FotoProdotto1.ContentLength > 0)
                {
                    prodotti.FotoProdotto1 = FotoProdotto1.FileName;
                    string pathToSave = Server.MapPath("~/Content/FileUpload/") + FotoProdotto1.FileName;
                    FotoProdotto1.SaveAs(pathToSave);
                }
                else
                {
                    prodotti.FotoProdotto1 = prodotto.FotoProdotto1;
                }
                if (FotoProdotto2 != null && FotoProdotto2.ContentLength > 0)
                {
                    prodotti.FotoProdotto2 = FotoProdotto2.FileName;
                    string pathToSave = Server.MapPath("~/Content/FileUpload/") + FotoProdotto2.FileName;
                    FotoProdotto2.SaveAs(pathToSave);
                }
                else
                {
                    prodotti.FotoProdotto2 = prodotto.FotoProdotto2;
                }
                if (FotoProdotto3 != null && FotoProdotto3.ContentLength > 0)
                {
                    prodotti.FotoProdotto3 = FotoProdotto3.FileName;
                    string pathToSave = Server.MapPath("~/Content/FileUpload/") + FotoProdotto3.FileName;
                    FotoProdotto3.SaveAs(pathToSave);
                }
                else
                {
                    prodotti.FotoProdotto3 = prodotto.FotoProdotto3;
                }
                db1.Entry(prodotti).State = EntityState.Modified;
                TempData["ProdottoModificato"] = "Modifiche salvate con successo";
                db1.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAzienda = new SelectList(db.Aziende, "IdAzienda", "NomeAzienda", prodotti.IdAzienda);
            ViewBag.IdCategoria = new SelectList(db.Categorie, "IdCategoria", "CategoriaProdotto", prodotti.IdCategoria);
            return View(prodotti);
        }

        // GET: Prodotti/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            var dettaglioOrdini = db.Dettaglio_Ordine.FirstOrDefault(x => x.IdProdotto == id);
            if(dettaglioOrdini != null && dettaglioOrdini.Ordine.StatoOrdine == "Ordine da evadere")
            {
                TempData["EliminaProdotto"] = "A questo prodotto è collegato un ordine, impossibile eliminare";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                //db.Prodotti.Remove(prodotti);
                TempData["EliminaProdotto"] = "Prodotto eliminato dall'elenco";
            }

            bool prodottiCorrelati = db.Scaffale_Prodotti.Any(x => x.IdProdotto == id);

            if(prodottiCorrelati)
            {
                TempData["ErroreEliminazione"] = "Elimina prima il prodotto dall'inventario magazzino";
                return RedirectToAction("Index", "Admin");
            }
            db.Prodotti.Remove(prodotti);
            TempData["EliminaProdotto"] = "Prodotto eliminato dall'elenco";
            db.SaveChanges();
            return RedirectToAction("Index","Prodotti");
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
