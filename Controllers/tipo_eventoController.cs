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
    public class tipo_eventoController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: tipo_evento
        public ActionResult Index()
        {
            worshipEntities eventoConn = new worshipEntities();
            foreach (var tipo_evento in db.tipo_evento)
            {
                var count = eventoConn.evento.Count(e => e.cd_tipo_evento == tipo_evento.cd_tipo_evento);
                tipo_evento.nrEventosUsando = count;
            }
            return View(db.tipo_evento.ToList());
        }

        // GET: tipo_evento/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_evento tipo_evento = db.tipo_evento.Find(id);
            if (tipo_evento == null)
            {
                return HttpNotFound();
            }
            return View(tipo_evento);
        }

        // GET: tipo_evento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipo_evento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tx_tipo_evento")] tipo_evento tipo_evento)
        {
            if (ModelState.IsValid)
            {
                db.tipo_evento.Add(tipo_evento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_evento);
        }

        // GET: tipo_evento/Edit/5
        public ActionResult Edit(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_evento tipo_evento = db.tipo_evento.Find(id);
            if (tipo_evento == null)
            {
                return HttpNotFound();
            }
            return View(tipo_evento);
        }

        // POST: tipo_evento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_tipo_evento,tx_tipo_evento")] tipo_evento tipo_evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_evento);
        }

        // GET: tipo_evento/Delete/5
        public ActionResult Delete(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_evento tipo_evento = db.tipo_evento.Find(id);
            if (tipo_evento == null)
            {
                return HttpNotFound();
            }
            return View(tipo_evento);
        }

        // POST: tipo_evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(sbyte? id)
        {
            tipo_evento tipo_evento = db.tipo_evento.Find(id);
            db.tipo_evento.Remove(tipo_evento);
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
