using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Linq;

namespace Worship.Models
{
    public class CardsViewModel
    {
        public evento ultimasMusicas { get; set; }
        public List<topicoQtde> topicosMaisCantados { get; set; }
        public List<musicaQtde> musicasMaisCantadas { get; set; }
    }
    public class musicaQtde
    {
        public string musica { get; set; }
        public int qtde { get; set; }
    }
    public class topicoQtde
    {
        public string topico { get; set; }
        public int qtde { get; set; }
    }
}