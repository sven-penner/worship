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

    public partial class evento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public evento()
        {
            this.evento_musicas = new HashSet<evento_musica>();
            this.evento_integrantes = new HashSet<evento_integrante>();
        }

        [Key]
        public short cd_evento { get; set; }
        [Required(ErrorMessage = "Informe a data do evento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data do evento")]
        public System.DateTime dt_evento { get; set; }
        [Required(ErrorMessage = "Informe o tipo de evento")]
        [Display(Name = "Tipo de evento")]
        public sbyte cd_tipo_evento { get; set; }
        [Required(ErrorMessage = "Informe a equipe")]
        [Display(Name = "Equipe")]
        public Nullable<sbyte> cd_equipe { get; set; }
        [StringLength(100, ErrorMessage = "Coment�rio deve ter no m�ximo 100 caracteres")]
        [Display(Name = "Coment�rio")]
        [DataType(DataType.MultilineText)]
        public string tx_comentarios { get; set; }

        public virtual tipo_evento tipo_evento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<evento_musica> evento_musicas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<evento_integrante> evento_integrantes { get; set; }
        public virtual equipe equipe { get; set; }
    }
}