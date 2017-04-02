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
    public class hinosController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: hinos
        public ActionResult Index()
        {
            var hino = db.hino.Include(h => h.tonalidade);
            worshipEntities hinoConn = new worshipEntities();
            foreach (var hinos in db.hino)
            {
                var count = hinoConn.evento_musica.Count(e => e.cd_hino == hinos.cd_hino);
                hinos.nrUtilizacoes = count;
            }
            return View(hino.ToList());
        }

        // GET: hinos/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hino hino = db.hino.Find(id);
            if (hino == null)
            {
                return HttpNotFound();
            }
            return View(hino);
        }

        // GET: hinos/Create
        public ActionResult Create()
        {
            ViewBag.tx_tonalidade = new SelectList(db.tonalidade, "tx_tonalidade", "tx_descricao_tonalidade");
            return View();
        }

        // POST: hinos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_hino,tx_titulo_hino,tx_inicio,tx_nome_compositor_letra,tx_nome_compositor_musica,nr_ano,tx_tonalidade")] hino hino)
        {
            if (ModelState.IsValid)
            {
                if (db.hino.Any(h => h.tx_titulo_hino == hino.tx_titulo_hino))
                    ModelState.AddModelError("musicaJaExiste", "Música já existe");
                else
                {
                    db.hino.Add(hino);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.tx_tonalidade = new SelectList(db.tonalidade, "tx_tonalidade", "tx_descricao_tonalidade", hino.tx_tonalidade);
            return View(hino);
        }

        // GET: hinos/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hino hino = db.hino.Find(id);
            if (hino == null)
            {
                return HttpNotFound();
            }
            ViewBag.tx_tonalidade = new SelectList(db.tonalidade, "tx_tonalidade", "tx_descricao_tonalidade", hino.tx_tonalidade);
            return View(hino);
        }

        // POST: hinos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_hino,tx_titulo_hino,tx_inicio,tx_nome_compositor_letra,tx_nome_compositor_musica,nr_ano,tx_tonalidade")] hino hino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tx_tonalidade = new SelectList(db.tonalidade, "tx_tonalidade", "tx_descricao_tonalidade", hino.tx_tonalidade);
            return View(hino);
        }

        // GET: hinos/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hino hino = db.hino.Find(id);
            if (hino == null)
            {
                return HttpNotFound();
            }
            return View(hino);
        }

        // POST: hinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            hino hino = db.hino.Find(id);
            db.hino.Remove(hino);
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
