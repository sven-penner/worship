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
    
    public partial class evento_musica
    {
        public System.DateTime dt_evento { get; set; }
        public int cd_hino { get; set; }
        public Nullable<sbyte> nr_sequencia { get; set; }
    
        public virtual evento evento { get; set; }
        public virtual hino hino { get; set; }
    }
}
