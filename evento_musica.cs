//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Worship
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class evento_musica
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "C�digo do evento")]
        public short cd_evento { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name = "M�sica")]
        public short cd_hino { get; set; }
        public Nullable<sbyte> nr_sequencia { get; set; }

        public virtual evento evento { get; set; }
        public virtual hino hino { get; set; }
    }
}