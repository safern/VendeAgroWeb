//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendeAgroWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Videos_Anuncio
    {
        public int id { get; set; }
        public string ruta { get; set; }
        public int idAnuncio { get; set; }
    
        public virtual Anuncio Anuncio { get; set; }
    }
}