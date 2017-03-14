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
    public class genero_musicaController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: genero_musica
        public ActionResult Index()
        {
            worshipEntities hinoGeneroConn = new worshipEntities();
            foreach (var genero_musica in db.genero_musica)
            {
                var count = hinoGeneroConn.hino_genero.Where(hg => hg.cd_genero == genero_musica.cd_genero_musica).Where(hg => hg.id_genero == "M").Count();
                genero_musica.nrHinosUsando = count;
            }
            return View(db.genero_musica.ToList());
        }

        // GET: genero_musica/Details/5
        public ActionResult Details(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            genero_musica genero_musica = db.genero_musica.Find(id);
            if (genero_musica == null)
            {
                return HttpNotFound();
            }
            return View(genero_musica);
        }

        // GET: genero_musica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: genero_musica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_genero_musica,tx_genero_musica")] genero_musica genero_musica)
        {
            if (ModelState.IsValid)
            {
                db.genero_musica.Add(genero_musica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genero_musica);
        }

        // GET: genero_musica/Edit/5
        public ActionResult Edit(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            genero_musica genero_musica = db.genero_musica.Find(id);
            if (genero_musica == null)
            {
                return HttpNotFound();
            }
            return View(genero_musica);
        }

        // POST: genero_musica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_genero_musica,tx_genero_musica")] genero_musica genero_musica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genero_musica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genero_musica);
        }

        // GET: genero_musica/Delete/5
        public ActionResult Delete(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            genero_musica genero_musica = db.genero_musica.Find(id);
            if (genero_musica == null)
            {
                return HttpNotFound();
            }
            return View(genero_musica);
        }

        // POST: genero_musica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(sbyte id)
        {
            genero_musica genero_musica = db.genero_musica.Find(id);
            db.genero_musica.Remove(genero_musica);
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
