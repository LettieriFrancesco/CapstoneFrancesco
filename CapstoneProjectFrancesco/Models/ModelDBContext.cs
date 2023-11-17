using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CapstoneProjectFrancesco.Models
{
    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=ModelDBContext")
        {
        }

        public virtual DbSet<Aziende> Aziende { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Corsie_Magazzino> Corsie_Magazzino { get; set; }
        public virtual DbSet<Dettaglio_Ordine> Dettaglio_Ordine { get; set; }
        public virtual DbSet<Ordine> Ordine { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<Richeste_Clienti> Richeste_Clienti { get; set; }
        public virtual DbSet<Scaffale_Magazzino> Scaffale_Magazzino { get; set; }
        public virtual DbSet<Scaffale_Prodotti> Scaffale_Prodotti { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aziende>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Aziende)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categorie>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Categorie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Corsie_Magazzino>()
                .HasMany(e => e.Scaffale_Magazzino)
                .WithRequired(e => e.Corsie_Magazzino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ordine>()
                .Property(e => e.Importo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ordine>()
                .HasMany(e => e.Dettaglio_Ordine)
                .WithRequired(e => e.Ordine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.Dettaglio_Ordine)
                .WithRequired(e => e.Prodotti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.Scaffale_Prodotti)
                .WithRequired(e => e.Prodotti)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Scaffale_Magazzino>()
                .HasMany(e => e.Scaffale_Prodotti)
                .WithRequired(e => e.Scaffale_Magazzino)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
