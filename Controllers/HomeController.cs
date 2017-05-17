using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Worship;
using Worship.Models;

namespace Worship.Controllers
{
    public class HomeController : Controller
    {
        private worshipEntities db = new worshipEntities();

        public ActionResult Index()
        {
            CardsViewModel cards = new CardsViewModel();
            // Músicas do último evento
            cards.ultimasMusicas = db.evento.OrderByDescending(e => e.dt_evento).ThenByDescending(e => e.cd_tipo_evento).FirstOrDefault();

            // Tópicos mais tocados
            List<evento_musica> generosMaisTocados = (from em in db.evento_musica
                                                     join h in db.hino on em.cd_hino equals h.cd_hino
                                                     join hg in db.hino_genero on h.cd_hino equals hg.cd_hino
                                                     join gl in db.genero_letra on hg.cd_genero equals gl.cd_genero_letra
                                                     where hg.id_genero == "L"
                                                     select em).ToList();
            return View(cards);
        }
    }
}