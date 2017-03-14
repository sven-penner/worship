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

    public partial class tonalidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tonalidade()
        {
            this.hinos = new HashSet<hino>();
        }
    
        [Key]
        [Required(ErrorMessage = "Digite a tonalidade")]
        [StringLength(3, ErrorMessage = "Tonalidade deve ter no m�ximo 3 caracteres")]
        [Display(Name = "Tonalidade")]
        public string tx_tonalidade { get; set; }
        [Required(ErrorMessage = "Digite a descri��o da tonalidade")]
        [StringLength(25, ErrorMessage = "Tonalidade deve ter no m�ximo 25 caracteres")]
        [Display(Name = "Descri��o da tonalidade")]
        public string tx_descricao_tonalidade { get; set; }
        [Required(ErrorMessage = "Informe a posi��o relativa da tonalidade (0=C;1=C#;...)")]
        [Range(0,11,ErrorMessage = "Posi��o deve ter valores entre 0 e 11")]
        [Display(Name = "Posi��o")]
        public sbyte nr_posicao { get; set; }
        [Required(ErrorMessage = "Informe a escala")]
        [Display(Name = "Escala")]
        public string ind_maior_menor { get; set; }

        [Display(Name = "Qt. M�sicas")]
        public int nrHinosUsando { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hino> hinos { get; set; }
    }
}
