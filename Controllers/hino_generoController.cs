using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Worship;

namespace Worship.Controllers
{
    public class hino_generoController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: hino_genero
        public ActionResult Index()
        {
            var hino_genero = db.hino_genero.Include(h => h.genero_musica).Include(h => h.genero_letra).Include(h => h.hino);
            return View(hino_genero.ToList());
        }

        // GET: hino_genero/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hino_genero hino_genero = db.hino_genero.Find(id);
            if (hino_genero == null)
            {
                return HttpNotFound();
            }
            return View(hino_genero);
        }

        // GET: hino_genero/Create
        public ActionResult Create()
        {
            ViewBag.cd_genero = new SelectList(db.genero_musica, "cd_genero_musica", "tx_genero_musica");
            ViewBag.cd_genero = new SelectList(db.genero_letra, "cd_genero_letra", "tx_genero_letra");
            ViewBag.cd_hino = new SelectList(db.hino, "cd_hino", "tx_titulo_hino");
            return View();
        }

        // POST: hino_genero/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_hino,cd_genero,id_genero")] hino_genero hino_genero)
        {
            if (ModelState.IsValid)
            {
                if (!db.hino_genero.Any(hg => hg.cd_hino == hino_genero.cd_hino && hg.cd_genero == hino_genero.cd_genero && hg.id_genero == hino_genero.id_genero)) {
                    db.hino_genero.Add(hino_genero);
                    db.SaveChanges();
                }
            }
            return Json(Url.Action("Edit", "hinos", new { id = hino_genero.cd_hino }));
        }

        // GET: hino_genero/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hino_genero hino_genero = db.hino_genero.Find(id);
            if (hino_genero == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_genero = new SelectList(db.genero_musica, "cd_genero_musica", "tx_genero_musica", hino_genero.cd_genero);
            ViewBag.cd_genero = new SelectList(db.genero_letra, "cd_genero_letra", "tx_genero_letra", hino_genero.cd_genero);
            ViewBag.cd_hino = new SelectList(db.hino, "cd_hino", "tx_titulo_hino", hino_genero.cd_hino);
            return View(hino_genero);
        }

        // POST: hino_genero/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_hino,cd_genero,id_genero")] hino_genero hino_genero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hino_genero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_genero = new SelectList(db.genero_musica, "cd_genero_musica", "tx_genero_musica", hino_genero.cd_genero);
            ViewBag.cd_genero = new SelectList(db.genero_letra, "cd_genero_letra", "tx_genero_letra", hino_genero.cd_genero);
            ViewBag.cd_hino = new SelectList(db.hino, "cd_hino", "tx_titulo_hino", hino_genero.cd_hino);
            return View(hino_genero);
        }

        // GET: hino_genero/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hino_genero hino_genero = db.hino_genero.Find(id);
            if (hino_genero == null)
            {
                return HttpNotFound();
            }
            return View(hino_genero);
        }

        // POST: hino_genero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "cd_hino,cd_genero,id_genero")] hino_genero hino_genero)
        {
            hino_genero hg = db.hino_genero.Find(hino_genero.cd_hino, hino_genero.cd_genero, hino_genero.id_genero);
            db.hino_genero.Remove(hg);
            db.SaveChanges();
            return Json(Url.Action("Edit", "hinos", new { id = hino_genero.cd_hino }));
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
