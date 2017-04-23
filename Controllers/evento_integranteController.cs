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
        public ActionResult Details(DateTime id)
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
            ViewBag.dt_evento = new SelectList(db.evento, "dt_evento", "tx_comentarios");
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante");
            return View();
        }

        // POST: evento_integrante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dt_evento,cd_integrante")] evento_integrante evento_integrante)
        {
            if (ModelState.IsValid)
            {
                db.evento_integrante.Add(evento_integrante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dt_evento = new SelectList(db.evento, "dt_evento", "tx_comentarios", evento_integrante.dt_evento);
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante", evento_integrante.cd_integrante);
            return View(evento_integrante);
        }

        // GET: evento_integrante/Edit/5
        public ActionResult Edit(DateTime id)
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
            ViewBag.dt_evento = new SelectList(db.evento, "dt_evento", "tx_comentarios", evento_integrante.dt_evento);
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante", evento_integrante.cd_integrante);
            return View(evento_integrante);
        }

        // POST: evento_integrante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dt_evento,cd_integrante")] evento_integrante evento_integrante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento_integrante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dt_evento = new SelectList(db.evento, "dt_evento", "tx_comentarios", evento_integrante.dt_evento);
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante", evento_integrante.cd_integrante);
            return View(evento_integrante);
        }

        // GET: evento_integrante/Delete/5
        public ActionResult Delete(DateTime id)
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
        public ActionResult DeleteConfirmed(DateTime id)
        {
            evento_integrante evento_integrante = db.evento_integrante.Find(id);
            db.evento_integrante.Remove(evento_integrante);
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
