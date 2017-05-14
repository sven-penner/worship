using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Worship;

namespace Worship.Controllers
{
    public class HomeController : Controller
    {
        private worshipEntities db = new worshipEntities();

        public ActionResult Index()
        {
            evento evento;
            evento = db.evento.OrderByDescending(e => e.dt_evento).ThenByDescending(e => e.cd_tipo_evento).FirstOrDefault();
            return View(evento);
        }
    }
}