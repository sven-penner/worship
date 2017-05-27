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
        public IList<genero_letra> generosMaisTocados { get; set; }
        public List<musicaQtde> musicasMaisTocadas { get; set; }
    }
    public class musicaQtde
    {
        public string musica { get; set; }
        public int qtde { get; set; }
    }
}