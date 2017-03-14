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
    public class tonalidadesController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: tonalidades
        public ActionResult Index()
        {
            worshipEntities hinoConn = new worshipEntities();
            foreach(var tonalidade in db.tonalidade)
            {
                var count = hinoConn.hino.Count(h => h.tx_tonalidade == tonalidade.tx_tonalidade);
                tonalidade.nrHinosUsando = count;
            }
            return View(db.tonalidade.ToList());
        }

        // GET: tonalidades/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tonalidade tonalidade = db.tonalidade.Find(id);
            if (tonalidade == null)
            {
                return HttpNotFound();
            }
            return View(tonalidade);
        }

        // GET: tonalidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tonalidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tx_tonalidade,tx_descricao_tonalidade,nr_posicao,ind_maior_menor")] tonalidade tonalidade)
        {
            if (ModelState.IsValid)
            {
                if (db.tonalidade.Any(t => t.tx_tonalidade == tonalidade.tx_tonalidade))
                   ModelState.AddModelError("tonalidadeJaExiste", "Tonalidade já existe");
                else
                {
                    db.tonalidade.Add(tonalidade);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(tonalidade);
        }

        // GET: tonalidades/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tonalidade tonalidade = db.tonalidade.Find(id);
            if (tonalidade == null)
            {
                return HttpNotFound();
            }
            return View(tonalidade);
        }

        // POST: tonalidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tx_tonalidade,tx_descricao_tonalidade,nr_posicao,ind_maior_menor")] tonalidade tonalidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tonalidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tonalidade);
        }

        // GET: tonalidades/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tonalidade tonalidade = db.tonalidade.Find(id);
            if (tonalidade == null)
            {
                return HttpNotFound();
            }
            return View(tonalidade);
        }

        // POST: tonalidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tonalidade tonalidade = db.tonalidade.Find(id);
            db.tonalidade.Remove(tonalidade);
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
