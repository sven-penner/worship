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
    public class equipe_integranteController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: equipe_integrante
        public ActionResult Index()
        {
            var equipe_integrante = db.equipe_integrante.Include(e => e.equipe).Include(e => e.integrante);
            return View(equipe_integrante.ToList());
        }

        // GET: equipe_integrante/Details/5
        public ActionResult Details(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipe_integrante equipe_integrante = db.equipe_integrante.Find(id);
            if (equipe_integrante == null)
            {
                return HttpNotFound();
            }
            return View(equipe_integrante);
        }

        // GET: equipe_integrante/Create
        public ActionResult Create()
        {
            ViewBag.cd_equipe = new SelectList(db.equipe, "cd_equipe", "tx_nome_equipe");
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante");
            return View();
        }

        // POST: equipe_integrante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_equipe,cd_integrante")] equipe_integrante equipe_integrante)
        {
            if (ModelState.IsValid)
            {
                db.equipe_integrante.Add(equipe_integrante);
                db.SaveChanges();
            }

            ViewBag.cd_equipe = new SelectList(db.equipe, "cd_equipe", "tx_nome_equipe", equipe_integrante.cd_equipe);
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante", equipe_integrante.cd_integrante);
            return RedirectToAction("Edit", "equipe_integrante", new { id = equipe_integrante.cd_equipe });
        }

        // GET: equipe_integrante/Edit/5
        public ActionResult Edit(sbyte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipe equipe = db.equipe.Find(id);
            if (equipe == null)
            {
                return HttpNotFound();
            }
            // Add the values to be used in the dropdown list
            var selectListItems = db.integrante.OrderBy(i => i.tx_nome_integrante).ToList().
                Select(i => new SelectListItem
                {
                    Text = i.tx_nome_integrante,
                    Value = i.cd_integrante.ToString()
                });

            // Concatenating with our DefaultItem so we have that classic "- SELECT -"
            var ddList = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            ViewBag.ddList = ddList;
            return View(equipe);
        }

        // POST: equipe_integrante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_equipe,cd_integrante")] equipe_integrante equipe_integrante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipe_integrante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cd_equipe = new SelectList(db.equipe, "cd_equipe", "tx_nome_equipe", equipe_integrante.cd_equipe);
            ViewBag.cd_integrante = new SelectList(db.integrante, "cd_integrante", "tx_nome_integrante", equipe_integrante.cd_integrante);
            return View(equipe_integrante);
        }

        // POST: equipe_integrante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "cd_equipe,cd_integrante")] equipe_integrante equipe_integrante)
        {
            equipe_integrante ei = db.equipe_integrante.Find(equipe_integrante.cd_equipe, equipe_integrante.cd_integrante);
            db.equipe_integrante.Remove(ei);
            db.SaveChanges();
            return Json(Url.Action("Edit", "equipe_integrante", new { id = equipe_integrante.cd_equipe }));
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
