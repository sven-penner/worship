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

    public partial class equipe_integrante
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Equipe")]
        public sbyte cd_equipe { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name = "Integrante")]
        public sbyte cd_integrante { get; set; }

        public virtual equipe equipe { get; set; }
        public virtual integrante integrante { get; set; }
    }
}