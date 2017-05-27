using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Worship;
using Worship.Models;
using System.Collections;

namespace Worship.Controllers
{
    public class HomeController : Controller
    {
        private worshipEntities db = new worshipEntities();

        public int qtde { get; private set; }

        public ActionResult Index()
        {
            CardsViewModel cards = new CardsViewModel();
            // Músicas do último evento
            cards.ultimasMusicas = db.evento.OrderByDescending(e => e.dt_evento).ThenByDescending(e => e.cd_tipo_evento).FirstOrDefault();

            // Tópicos mais tocados
            //List<evento_musica> generosMaisTocados = (from em in db.evento_musica
            //                                         join h in db.hino on em.cd_hino equals h.cd_hino
            //                                         join hg in db.hino_genero on h.cd_hino equals hg.cd_hino
            //                                         join gl in db.genero_letra on hg.cd_genero equals gl.cd_genero_letra
            //                                         where hg.id_genero == "L"
            //                                         select em).GroupBy(em => em.cd_hino).Count().ToList();

            var musicasMaisTocadas = (from em in db.evento_musica
                                     join m in db.hino on em.cd_hino equals m.cd_hino
                                     group em by m.tx_titulo_hino into g
                                     select new { musica = g.Key, qtde = g.Count() }).OrderByDescending(g => g.qtde).ToList();
            cards.musicasMaisTocadas = musicasMaisTocadas.Select(m => new Models.musicaQtde { musica = m.musica, qtde = m.qtde }).ToList();
            
            return View(cards);
        }
    }
}