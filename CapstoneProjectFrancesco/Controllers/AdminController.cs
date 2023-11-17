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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ModelDBContext db = new ModelDBContext();
        // GET: Admin
        // Ordino la lista degli ordini con lo stato dell'ordine ancora da evadere.
        public ActionResult Index()
        {
            var ordini = db.Dettaglio_Ordine.ToList();
            if(ordini.Count > 0)
            {
            return View(db.Dettaglio_Ordine.OrderByDescending(x=> x.Ordine.StatoOrdine == "Ordine da evadere").ToList());

            }
            else
            {
                TempData["NessunOrdine"] = "Nessun ordine in arrivo"; 
               
            }
            return View(ordini);
        }
        //Recupero l'ID dell'ordine per effettuare le modifiche sull'ordine.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordine ordine = db.Ordine.Find(id);
            if (ordine == null)
            {
                return HttpNotFound();
            }
            //Passo i dati dell'User nella ViewBag.
            ViewBag.IdUser = new SelectList(db.User, "IdUser", "Nome", ordine.IdUser);
            return View(ordine);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Mando in post le modifiche e le salvo nel db.
        public ActionResult Edit(Ordine ordine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.User, "IdUser", "Nome", ordine.IdUser);
            return View(ordine);
        }

    }
}