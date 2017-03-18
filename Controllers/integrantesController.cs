using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Worship.Models;

namespace Worship
{
    public class integrantesController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: integrantes
        public ActionResult Index()
        {
            return View(db.integrante.ToList());
        }

        // GET: integrantes/Details/5
        public ActionResult Details(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            integrante integrante = db.integrante.Find(id);
            if (integrante == null)
            {
                return HttpNotFound();
            }
            return View(integrante);
        }

        // GET: integrantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: integrantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_integrante,tx_nome_integrante,tx_email_integrante,tx_nome_curto_integrante")] integrante integrante)
        {
            if (ModelState.IsValid)
            {
                if (db.integrante.Any(i => i.tx_nome_integrante == integrante.tx_nome_integrante))
                    ModelState.AddModelError("integranteJaExiste", "Integrante já existe");
                else
                {
                    db.integrante.Add(integrante);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(integrante);
        }

        // GET: integrantes/Edit/5
        public ActionResult Edit(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            integrante integrante = db.integrante.Find(id);
            if (integrante == null)
            {
                return HttpNotFound();
            }
            return View(integrante);
        }

        // POST: integrantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_integrante,tx_nome_integrante,tx_email_integrante,tx_nome_curto_integrante")] integrante integrante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(integrante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(integrante);
        }

        // GET: integrantes/Delete/5
        public ActionResult Delete(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            integrante integrante = db.integrante.Find(id);
            if (integrante == null)
            {
                return HttpNotFound();
            }
            return View(integrante);
        }

        // POST: integrantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(sbyte id)
        {
            integrante integrante = db.integrante.Find(id);
            db.integrante.Remove(integrante);
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
