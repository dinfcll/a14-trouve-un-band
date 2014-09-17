using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TrouveUnBand.Models
{
    public class Users
    {
        [Key]
        public int IDUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }
    }
    public class Band
    {
        [Key]
        public int IDBand { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
    
    public class Musician
    {
        [Key]
        public int IDMusician { get; set; }      
        public string Instrument { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [ForeignKey("Users")]
        public int IDUser { get; set; }
        public virtual Users Users { get; set; }
    }
    
    public class Join_Musician_Band
    {  
        [Key, Column(Order=1)]
        [ForeignKey("Musician")]
        public int IDMusician { get; set; }
        public virtual Musician Musician { get; set; }
        [Key, Column(Order=2)]
        [ForeignKey("Band")]
        public int IDBand { get; set; }
        public virtual Band Band { get; set; }
    }

    public class DBTUBContext : DbContext
    {
        public DbSet<Users> User { get; set; }
        public DbSet<Band> Band { get; set; }
        public DbSet<Musician> Musician { get; set; }
        public DbSet<Join_Musician_Band> Join_Musician_Band { get; set; }
    }
}


