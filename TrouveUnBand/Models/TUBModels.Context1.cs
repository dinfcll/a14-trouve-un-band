﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TrouveUnBand.Models
{
    public partial class TrouveUnBandEntities1 : DbContext
    {
        public TrouveUnBandEntities1()
            : base("name=TrouveUnBandEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Band> Bands { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Join_Musician_Instrument> Join_Musician_Instrument { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<User> Users { get; set; }
    }
}