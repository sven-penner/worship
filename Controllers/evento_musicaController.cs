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
    public class evento_musicaController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: evento_musica
        public ActionResult Index()
        {
            var evento_musica = db.evento_musica.Include(e => e.evento).Include(e => e.hino);
            return View(evento_musica.ToList());
        }

        // GET: evento_musica/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento_musica evento_musica = db.evento_musica.Find(id);
            if (evento_musica == null)
            {
                return HttpNotFound();
            }
            return View(evento_musica);
        }

        // GET: evento_musica/Create
        public ActionResult Create()
        {
            ViewBag.dt_evento = new SelectList(db.evento, "dt_evento", "tx_comentarios");
            ViewBag.cd_hino = new SelectList(db.hino, "cd_hino", "tx_titulo_hino");
            return View();
        }

        // POST: evento_musica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dt_evento,cd_hino,nr_sequencia")] evento_musica evento_musica)
        {
            if (ModelState.IsValid)
            {
                db.evento_musica.Add(evento_musica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dt_evento = new SelectList(db.evento, "dt_evento", "tx_comentarios", evento_musica.dt_evento);
            ViewBag.cd_hino = new SelectList(db.hino, "cd_hino", "tx_titulo_hino", evento_musica.cd_hino);
            return View(evento_musica);
        }

        // GET: evento_musica/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento_musica evento_musica = db.evento_musica.Find(id);
            if (evento_musica == null)
            {
                return HttpNotFound();
            }
            ViewBag.dt_evento = new SelectList(db.evento, "dt_evento", "tx_comentarios", evento_musica.dt_evento);
            ViewBag.cd_hino = new SelectList(db.hino, "cd_hino", "tx_titulo_hino", evento_musica.cd_hino);
            return View(evento_musica);
        }

        // POST: evento_musica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dt_evento,cd_hino,nr_sequencia")] evento_musica evento_musica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento_musica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dt_evento = new SelectList(db.evento, "dt_evento", "tx_comentarios", evento_musica.dt_evento);
            ViewBag.cd_hino = new SelectList(db.hino, "cd_hino", "tx_titulo_hino", evento_musica.cd_hino);
            return View(evento_musica);
        }

        // GET: evento_musica/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento_musica evento_musica = db.evento_musica.Find(id);
            if (evento_musica == null)
            {
                return HttpNotFound();
            }
            return View(evento_musica);
        }

        // POST: evento_musica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            evento_musica evento_musica = db.evento_musica.Find(id);
            db.evento_musica.Remove(evento_musica);
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
