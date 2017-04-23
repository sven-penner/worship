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
    public class eventosController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: eventos
        public ActionResult Index()
        {
            var evento = db.evento.Include(e => e.tipo_evento).Include(e => e.equipe);
            return View(evento.ToList());
        }

        // GET: eventos/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento evento = db.evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: eventos/Create
        public ActionResult Create()
        {
            ViewBag.cd_tipo_evento = new SelectList(db.tipo_evento, "cd_tipo_evento", "tx_tipo_evento");
            ViewBag.cd_equipe = new SelectList(db.equipe, "cd_equipe", "tx_nome_equipe");
            return View();
        }

        // POST: eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dt_evento,cd_tipo_evento,cd_equipe,tx_comentarios")] evento evento)
        {
            if (ModelState.IsValid)
            {
                db.evento.Add(evento);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = evento.dt_evento });
            }

            ViewBag.cd_tipo_evento = new SelectList(db.tipo_evento, "cd_tipo_evento", "tx_tipo_evento", evento.cd_tipo_evento);
            ViewBag.cd_equipe = new SelectList(db.equipe, "cd_equipe", "tx_nome_equipe", evento.cd_equipe);
            return View(evento);
        }

        // GET: eventos/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento evento = db.evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            ViewBag.cd_tipo_evento = new SelectList(db.tipo_evento, "cd_tipo_evento", "tx_tipo_evento", evento.cd_tipo_evento);
            ViewBag.cd_equipe = new SelectList(db.equipe, "cd_equipe", "tx_nome_equipe", evento.cd_equipe);
            // Add the values to be used in the dropdown list
            var selectListItems = db.hino.OrderBy(h => h.tx_titulo_hino).ToList().
                Select(h => new SelectListItem
                {
                    Text = h.tx_titulo_hino,
                    Value = h.cd_hino.ToString()
                });

            // Concatenating with our DefaultItem so we have that classic "- SELECT -"
            var ddList = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            ViewBag.ddList = ddList;
            return View(evento);
        }

        // POST: eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dt_evento,cd_tipo_evento,cd_equipe,tx_comentarios")] evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_tipo_evento = new SelectList(db.tipo_evento, "cd_tipo_evento", "tx_tipo_evento", evento.cd_tipo_evento);
            ViewBag.cd_equipe = new SelectList(db.equipe, "cd_equipe", "tx_nome_equipe", evento.cd_equipe);
            return View(evento);
        }

        // GET: eventos/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evento evento = db.evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            evento evento = db.evento.Find(id);
            db.evento.Remove(evento);
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
        public IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "",
                    Text = "-- Selecione --"
                }, count: 1);
            }
        }
    }
}
