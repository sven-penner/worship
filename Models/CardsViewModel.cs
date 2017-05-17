using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Worship.Models
{
    public class CardsViewModel
    {
        public evento ultimasMusicas { get; set; }
        public IList<genero_letra> generosMaisTocados { get; set; }
    }
}