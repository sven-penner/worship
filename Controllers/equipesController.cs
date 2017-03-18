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
    public class equipesController : Controller
    {
        private worshipEntities db = new worshipEntities();

        // GET: equipes
        public ActionResult Index()
        {
            worshipEntities integrantesConn = new worshipEntities();
            foreach (var equipe in db.equipe)
            {
                var count = integrantesConn.equipe_integrante.Where(ei => ei.cd_equipe == equipe.cd_equipe).Count();
                equipe.nrIntegrantes = count;
            }
            return View(db.equipe.OrderBy(e=>e.nr_ano).ThenBy(e=>e.nr_domingo).ToList());
        }

        // GET: equipes/Details/5
        public ActionResult Details(sbyte? id)
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
            return View(equipe);
        }

        // GET: equipes/Create
        public ActionResult Create()
        {
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
            return View();
        }

        // POST: equipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_equipe,tx_nome_equipe,nr_ano,nr_domingo,cd_integrante_lider")] equipe equipe)
        {
            if (ModelState.IsValid)
            {
                if (db.equipe.Any(e => e.tx_nome_equipe == equipe.tx_nome_equipe && e.nr_ano == equipe.nr_ano))
                    ModelState.AddModelError("equipeJaExiste", "Equipe já existe para o ano indicado");
                else
                {
                    db.equipe.Add(equipe);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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

        // GET: equipes/Edit/5
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

        // POST: equipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_equipe,tx_nome_equipe,nr_ano,nr_domingo,cd_integrante_lider")] equipe equipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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

        // GET: equipes/Delete/5
        public ActionResult Delete(sbyte? id)
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
            return View(equipe);
        }

        // POST: equipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(sbyte id)
        {
            equipe equipe = db.equipe.Find(id);
            db.equipe.Remove(equipe);
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
        //private integranteViewModel AllTypeItems(string[] selectedValues = null)
        //{
        //    var ddList = new integranteViewModel();

        //    // Add the values to be used in the checkbox list
        //    // The values can come from anywhere
        //    var selectListItems = db.integrante.OrderBy(i => i.tx_nome_integrante).ToList().Select(i => new SelectListItem
        //    {
        //        Text = i.tx_nome_integrante,
        //        Value = i.cd_integrante.ToString()
        //    });

        //    // Concatenating with our DefaultItem so we have that classic "- SELECT -"
        //    ddList.SelectListItems = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");

        //    if (selectedValues != null)
        //    {
        //        ddList.SelectedValues = selectedValues;
        //    }

        //    return ddList;
        //}

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
