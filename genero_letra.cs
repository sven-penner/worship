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

    public partial class genero_letra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public genero_letra()
        {
            this.hino_generos = new HashSet<hino_genero>();
        }

        [Key]
        [Required]
        public sbyte cd_genero_letra { get; set; }
        [Required(ErrorMessage = "Digite o t�pico")]
        [StringLength(25, ErrorMessage = "T�pico deve ter no m�ximo 25 caracteres")]
        [Display(Name = "T�pico")]
        public string tx_genero_letra { get; set; }

        [Display(Name = "Qt. M�sicas")]
        public int nrHinosUsando { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hino_genero> hino_generos { get; set; }
    }
}