﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VendeAgroEntities : DbContext
    {
        public VendeAgroEntities()
            : base("name=VendeAgroEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Anuncio> Anuncios { get; set; }
        public virtual DbSet<Anuncio_Beneficio> Anuncio_Beneficio { get; set; }
        public virtual DbSet<Anuncio_Paquete> Anuncio_Paquete { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Beneficio> Beneficios { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<Conekta_Paquete> Conekta_Paquete { get; set; }
        public virtual DbSet<Estado> Estadoes { get; set; }
        public virtual DbSet<Fotos_Anuncio> Fotos_Anuncio { get; set; }
        public virtual DbSet<Metrica> Metricas { get; set; }
        public virtual DbSet<Pago> Pagoes { get; set; }
        public virtual DbSet<Pago_Concepto> Pago_Concepto { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<Paquete> Paquetes { get; set; }
        public virtual DbSet<Subcategoria> Subcategorias { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Usuario_Administrador> Usuario_Administrador { get; set; }
        public virtual DbSet<Usuario_Tarjeta> Usuario_Tarjeta { get; set; }
        public virtual DbSet<Videos_Anuncio> Videos_Anuncio { get; set; }
    }
}
