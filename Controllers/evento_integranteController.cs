using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Worship
{
    public class evento_integranteController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: evento_integrante
        public ActionResult Index()
        {
            var evento_integrante = db.evento_integrante.Include(e => e.evento).Include(e => e.integrante);
            return View(evento_integrante.ToList());
        }

        // GET: evento_integrante/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento_integrante evento_integrante = db.evento_integrante.Find(id);
            if (evento_integrante == null)
            {
                return HttpNotFound();
            }
            return View(evento_integrante);
        }

        // GET: evento_integrante/Create
        public ActionResult Create()
        {
            ViewBag.cd_evento = new SelectList(db.evento, "cd_evento", "tx_comentarios");
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante");
            return View();
        }

        // POST: evento_integrante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_evento,cd_integrante")] evento_integrante evento_integrante)
        {
            if (ModelState.IsValid)
            {
                if (!db.evento_integrante.Any(ei => ei.cd_evento == evento_integrante.cd_evento && ei.cd_integrante == evento_integrante.cd_integrante))
                {
                    db.evento_integrante.Add(evento_integrante);
                    db.SaveChanges();
                }
            }
            return Json(Url.Action("Edit", "eventos", new { id = evento_integrante.cd_evento }));
        }

        // GET: evento_integrante/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento_integrante evento_integrante = db.evento_integrante.Find(id);
            if (evento_integrante == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_evento = new SelectList(db.evento, "cd_evento", "tx_comentarios", evento_integrante.cd_evento);
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante", evento_integrante.cd_integrante);
            return View(evento_integrante);
        }

        // POST: evento_integrante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_evento,cd_integrante")] evento_integrante evento_integrante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento_integrante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_evento = new SelectList(db.evento, "cd_evento", "tx_comentarios", evento_integrante.cd_evento);
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante", evento_integrante.cd_integrante);
            return View(evento_integrante);
        }

        // GET: evento_integrante/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento_integrante evento_integrante = db.evento_integrante.Find(id);
            if (evento_integrante == null)
            {
                return HttpNotFound();
            }
            return View(evento_integrante);
        }

        // POST: evento_integrante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "cd_evento,cd_integrante")] evento_integrante evento_integrante)
        {
            evento_integrante ei = db.evento_integrante.Find(Convert.ToSByte(evento_integrante.cd_integrante), Convert.ToInt16(evento_integrante.cd_evento));
            db.evento_integrante.Remove(ei);
            db.SaveChanges();
            return Json(Url.Action("Edit", "eventos", new { id = evento_integrante.cd_evento }));
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
