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
            worshipEntities conn = new worshipEntities();
            foreach (var integrante in db.integrante)
            {
                var count = 0;
                count += conn.equipe.Count(e => e.cd_integrante_lider == integrante.cd_integrante);
                count += conn.equipe_integrante.Count(ei => ei.cd_integrante == integrante.cd_integrante);
                count += conn.evento_integrante.Count(evi => evi.cd_integrante == integrante.cd_integrante);
                integrante.integrante_em_uso = (count > 0);
            }

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
        public ActionResult Create([Bind(Include = "cd_integrante,tx_nome_integrante,tx_email_integrante,tx_nome_curto_integrante")] integrante integrante, HttpPostedFileBase upload_foto)
        {
            if (ModelState.IsValid)
            {
                if (db.integrante.Any(i => i.tx_nome_integrante == integrante.tx_nome_integrante))
                    ModelState.AddModelError("integranteJaExiste", "Integrante já existe");
                else
                {
                    if (upload_foto != null)
                    {
                        integrante.foto_integrante = new byte[upload_foto.ContentLength];
                        upload_foto.InputStream.Read(integrante.foto_integrante, 0, upload_foto.ContentLength);
                    }
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
        public ActionResult Edit([Bind(Include = "cd_integrante,tx_nome_integrante,tx_email_integrante,tx_nome_curto_integrante")] integrante integrante, HttpPostedFileBase upload_foto = null)
        {
            if (ModelState.IsValid)
            {
                integrante integranteAntesDeSalvar = db.integrante.AsNoTracking().Where(i => i.cd_integrante == integrante.cd_integrante).First();
                byte[] foto = integranteAntesDeSalvar.foto_integrante;
                integranteAntesDeSalvar = null;

                db.Entry(integrante).State = EntityState.Modified;

                if (upload_foto != null)
                {
                    integrante.foto_integrante = new byte[upload_foto.ContentLength];
                    upload_foto.InputStream.Read(integrante.foto_integrante, 0, upload_foto.ContentLength);
                } else
                {
                    integrante.foto_integrante = foto;
                }
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

        public ActionResult GetImage(sbyte? id)
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
            byte[] imageData = integrante.foto_integrante;
            if (imageData != null && imageData.Length > 0)
            {
                //return File(imageData, "image/jpg");
                return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/jpeg");
            } else
            {
                return null;
            }
        }

        // POST: integrantes/DelImage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelImage(sbyte? id)
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
            integrante.foto_integrante = null;
            db.Entry(integrante).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", "integrantes", new { id = integrante.cd_integrante });
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
