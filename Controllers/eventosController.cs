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
        public ActionResult Details(short? id)
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

        // GET: eventos/Create/<date>
        public ActionResult Create(DateTime? dt_evento)
        {
            // Lista de tipos de eventos
            var selectListItems = new SelectList(db.tipo_evento.OrderBy(te => te.tx_tipo_evento).ToList(), "cd_tipo_evento", "tx_tipo_evento");
            ViewBag.ddTiposEvento = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            // Lista de equipes
            selectListItems = new SelectList(db.equipe.Where(e => e.nr_ano == DateTime.Now.Year).OrderBy(e => e.nr_domingo).ToList(), "cd_equipe", "tx_nome_equipe");
            ViewBag.ddEquipes = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");

            ViewBag.dt_evento = dt_evento;
            return View();
        }

        // POST: eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cd_evento,dt_evento,cd_tipo_evento,cd_equipe,tx_comentarios")] evento evento)
        {
            if (ModelState.IsValid)
            {
                if (db.evento.Any(e => e.dt_evento == evento.dt_evento && e.cd_tipo_evento == evento.cd_tipo_evento))
                    ModelState.AddModelError("eventoJaExiste", "Evento já cadastrado para esta data");
                else
                {
                    db.evento.Add(evento);
                    // Adiciona integrantes da equipe
                    foreach (var ei in db.equipe_integrante.Where(e => e.cd_equipe == evento.cd_equipe))
                    {
                        evento_integrante ev_int = new evento_integrante();
                        ev_int.cd_evento = evento.cd_evento;
                        ev_int.cd_integrante = ei.cd_integrante;
                        db.evento_integrante.Add(ev_int);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Edit", new { id = evento.cd_evento });
                }
            }
            // Lista de tipos de eventos
            var selectListItems = new SelectList(db.tipo_evento.OrderBy(te => te.tx_tipo_evento).ToList(), "cd_tipo_evento", "tx_tipo_evento");
            ViewBag.ddTiposEvento = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            // Lista de equipes
            selectListItems = new SelectList(db.equipe.Where(e => e.nr_ano == DateTime.Now.Year).OrderBy(e => e.nr_domingo).ToList(), "cd_equipe", "tx_nome_equipe");
            ViewBag.ddEquipes = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            // Lista de músicas
            selectListItems = new SelectList(db.hino.OrderBy(h => h.tx_titulo_hino).ToList(), "cd_hino", "tx_titulo_hino");
            ViewBag.ddMusicas = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            // Lista de integrantes
            selectListItems = new SelectList(db.integrante.OrderBy(i => i.tx_nome_integrante).ToList(), "cd_integrante", "tx_nome_integrante");
            ViewBag.ddIntegrantes = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            return View(evento);
        }

        // GET: eventos/EditLastEvent
        [HttpGet, ActionName("EditLastEvent")]
        public ActionResult Edit()
        {
            evento evento;
            evento = db.evento.OrderByDescending(e => e.dt_evento).ThenByDescending(e => e.cd_tipo_evento).FirstOrDefault();
            if (evento == null)
            {
                return RedirectToAction("Create", "eventos");
            }
            else
            {
                return RedirectToAction("Edit", "eventos", new { id = evento.cd_evento });
            }
        }

        // GET: eventos/Edit/<date>
        [HttpGet, ActionName("EditByDate")]
        public ActionResult Edit(string dt_evento_string)
        {
            evento evento;

            // Converte parâmetro texto em data
            DateTime dt_evento_datetime = new DateTime();
            if (dt_evento_string != null)
            {
                dt_evento_datetime = DateTime.ParseExact(dt_evento_string, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                evento = db.evento.Where(e => e.dt_evento == dt_evento_datetime).OrderByDescending(e => e.dt_evento).ThenByDescending(e => e.cd_tipo_evento).FirstOrDefault();
            } else
            {
                evento = db.evento.OrderByDescending(e => e.dt_evento).ThenByDescending(e => e.cd_tipo_evento).FirstOrDefault();
            }
            if (evento == null)
            {
                return Json(Url.Action("Create", "eventos", new { dt_evento = dt_evento_datetime }), JsonRequestBehavior.AllowGet);
            } else
            {
                return Json(Url.Action("Edit", "eventos", new { id = evento.cd_evento }), JsonRequestBehavior.AllowGet);
            }
        }

        // GET: eventos/Edit/5
        public ActionResult Edit(short id)
        {
            evento evento = db.evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }

            DateTime dt_evento = evento.dt_evento;
            // Seleciona anterior
            evento eventoAnterior = db.evento.Where(e => (e.dt_evento == dt_evento && e.cd_tipo_evento < evento.cd_tipo_evento) || (e.dt_evento < dt_evento)).OrderByDescending(e => e.dt_evento).ThenByDescending(e => e.cd_tipo_evento).FirstOrDefault();
            if (eventoAnterior != null)
            {
                ViewBag.eventoAnterior = eventoAnterior.cd_evento;
            }
            // Seleciona próximo
            evento proxEvento = db.evento.Where(e => (e.dt_evento == dt_evento && e.cd_tipo_evento > evento.cd_tipo_evento) || (e.dt_evento > dt_evento)).OrderBy(e => e.dt_evento).ThenBy(e => e.cd_tipo_evento).FirstOrDefault();
            if (proxEvento != null)
            {
                ViewBag.proxEvento = proxEvento.cd_evento;
            }

            // Lista de equipes
            var selectListItems = new SelectList(db.equipe.Where(e => e.nr_ano == DateTime.Now.Year).OrderBy(e => e.nr_domingo).ToList(), "cd_equipe", "tx_nome_equipe", evento.cd_equipe);
            ViewBag.ddEquipes = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            // Lista de músicas
            selectListItems = new SelectList(db.hino.OrderBy(h => h.tx_titulo_hino).ToList(), "cd_hino", "tx_titulo_hino");
            ViewBag.ddMusicas = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            // Lista de integrantes
            selectListItems = new SelectList(db.integrante.OrderBy(i => i.tx_nome_integrante).ToList(), "cd_integrante", "tx_nome_integrante");
            ViewBag.ddIntegrantes = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");

            return View(evento);
        }

        // POST: eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cd_evento,dt_evento,cd_tipo_evento,cd_equipe,tx_comentarios")] evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
            }
            DateTime dt_evento = evento.dt_evento;
            // Seleciona anterior
            evento eventoAnterior = db.evento.Where(e => (e.dt_evento == dt_evento && e.cd_tipo_evento < evento.cd_tipo_evento) || (e.dt_evento < dt_evento)).OrderByDescending(e => e.dt_evento).ThenByDescending(e => e.cd_tipo_evento).FirstOrDefault();
            if (eventoAnterior != null)
            {
                ViewBag.eventoAnterior = eventoAnterior.cd_evento;
            }
            // Seleciona próximo
            evento proxEvento = db.evento.Where(e => (e.dt_evento == dt_evento && e.cd_tipo_evento > evento.cd_tipo_evento) || (e.dt_evento > dt_evento)).OrderBy(e => e.dt_evento).ThenBy(e => e.cd_tipo_evento).FirstOrDefault();
            if (proxEvento != null)
            {
                ViewBag.proxEvento = proxEvento.cd_evento;
            }

            // Lista de equipes
            var selectListItems = new SelectList(db.equipe.Where(e => e.nr_ano == DateTime.Now.Year).OrderBy(e => e.nr_domingo).ToList(), "cd_equipe", "tx_nome_equipe", evento.cd_equipe);
            ViewBag.ddEquipes = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            // Lista de músicas
            selectListItems = new SelectList(db.hino.OrderBy(h => h.tx_titulo_hino).ToList(), "cd_hino", "tx_titulo_hino");
            ViewBag.ddMusicas = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");
            // Lista de integrantes
            selectListItems = new SelectList(db.integrante.OrderBy(i => i.tx_nome_integrante).ToList(), "cd_integrante", "tx_nome_integrante");
            ViewBag.ddIntegrantes = new SelectList(DefaultItem.Concat(selectListItems), "Value", "Text");

            return View(evento);
        }

        // GET: eventos/Delete/5
        public ActionResult Delete(short? id)
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
        public ActionResult DeleteConfirmed(short? id)
        {
            evento evento = db.evento.Find(id);
            db.evento_integrante.RemoveRange(db.evento_integrante.Where(ei => ei.cd_evento == evento.cd_evento));
            db.evento_musica.RemoveRange(db.evento_musica.Where(em => em.cd_evento == evento.cd_evento));
            db.evento.Remove(evento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: eventos/MoverMusica/<evento><musica><direção>
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult MoverMusica(short? cd_evento, short? cd_hino, string dir)
        {
            if (cd_evento == null || cd_hino == null || dir == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            evento_musica musicaMover = db.evento_musica.Where(em => em.cd_evento == cd_evento && em.cd_hino == cd_hino).First();
            if (musicaMover == null)
            {
                return HttpNotFound();
            }
            // Mover para cima (up)
            if (dir == "up")
            {
                evento_musica musicaAnterior = db.evento_musica.Where(em => em.cd_evento == cd_evento && em.nr_sequencia < musicaMover.nr_sequencia).OrderByDescending(em => em.nr_sequencia).First();
                musicaMover.nr_sequencia--;
                while (musicaAnterior.nr_sequencia <= musicaMover.nr_sequencia)
                {
                    musicaAnterior.nr_sequencia++;
                }
            } else // down
            {
                evento_musica proxMusica = db.evento_musica.Where(em => em.cd_evento == cd_evento && em.nr_sequencia > musicaMover.nr_sequencia).OrderBy(em => em.nr_sequencia).First();
                musicaMover.nr_sequencia++;
                while (proxMusica.nr_sequencia >= musicaMover.nr_sequencia)
                {
                    proxMusica.nr_sequencia--;
                }
            }
            db.SaveChanges();
            evento evento = db.evento.Find(cd_evento);
            return RedirectToAction("Edit", new { id = evento.cd_evento });
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
