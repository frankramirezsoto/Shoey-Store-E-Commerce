﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace shoeyStore.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShoeyDatabaseEntities : DbContext
    {
        public ShoeyDatabaseEntities()
            : base("name=ShoeyDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Calificacione> Calificaciones { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<DetallesOrden> DetallesOrdens { get; set; }
        public virtual DbSet<Orden> Ordens { get; set; }
        public virtual DbSet<Producto> Productoes { get; set; }
        public virtual DbSet<Vendedor> Vendedors { get; set; }
    }
}
