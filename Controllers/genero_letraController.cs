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
    public class genero_letraController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: genero_letra
        public ActionResult Index()
        {
            worshipEntities hinoGeneroConn = new worshipEntities();
            foreach (var genero_letra in db.genero_letra)
            {
                var count = hinoGeneroConn.hino_genero.Where(hg => hg.cd_genero == genero_letra.cd_genero_letra).Where(hg=>hg.id_genero =="L").Count();
                genero_letra.nrHinosUsando = count;
            }
            return View(db.genero_letra.ToList());
        }

        // GET: genero_letra/Details/5
        public ActionResult Details(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            genero_letra genero_letra = db.genero_letra.Find(id);
            if (genero_letra == null)
            {
                return HttpNotFound();
            }
            return View(genero_letra);
        }

        // GET: genero_letra/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: genero_letra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_genero_letra,tx_genero_letra")] genero_letra genero_letra)
        {
            if (ModelState.IsValid)
            {
                if (db.genero_letra.Any(g => g.tx_genero_letra == genero_letra.tx_genero_letra))
                    ModelState.AddModelError("topicoJaExiste", "Tópico já existe");
                else
                {
                    db.genero_letra.Add(genero_letra);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(genero_letra);
        }

        // GET: genero_letra/Edit/5
        public ActionResult Edit(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            genero_letra genero_letra = db.genero_letra.Find(id);
            if (genero_letra == null)
            {
                return HttpNotFound();
            }
            return View(genero_letra);
        }

        // POST: genero_letra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_genero_letra,tx_genero_letra")] genero_letra genero_letra)
        {
            if (ModelState.IsValid)
            {
                if (db.genero_letra.Any(g => g.tx_genero_letra == genero_letra.tx_genero_letra))
                    ModelState.AddModelError("topicoJaExiste", "Tópico já existe");
                else
                {
                    db.Entry(genero_letra).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(genero_letra);
        }

        // GET: genero_letra/Delete/5
        public ActionResult Delete(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            genero_letra genero_letra = db.genero_letra.Find(id);
            if (genero_letra == null)
            {
                return HttpNotFound();
            }
            return View(genero_letra);
        }

        // POST: genero_letra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(sbyte id)
        {
            genero_letra genero_letra = db.genero_letra.Find(id);
            db.genero_letra.Remove(genero_letra);
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
