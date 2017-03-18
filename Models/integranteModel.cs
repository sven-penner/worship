using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Worship.Models
{
    public class integranteViewModel
    {
        public string SelectedValue { get; set; }
        public string[] SelectedValues { get; set; }
        public SelectList SelectListItems { get; set; }
    }
    public class integranteModel
    {
        public sbyte cd_integrante { get; set; }
        public string tx_nome_integrante { get; set; }
    }


}