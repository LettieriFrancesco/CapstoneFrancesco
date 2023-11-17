namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            Dettaglio_Ordine = new HashSet<Dettaglio_Ordine>();
            Scaffale_Prodotti = new HashSet<Scaffale_Prodotti>();
        }

        [Key]
        public int IdProdotto { get; set; }

        [Required]
        [Display(Name = "Nome prodotto")]
        public string NomeProdotto { get; set; }

        [Required]
        [Display(Name = "Descrizione prodotto")]
        public string DescrizioneProdotto { get; set; }
        [Display(Name = "Quantità")]
        public int Quantita { get; set; }

        public string Ingredienti { get; set; }

        public string Conservazione { get; set; }

        public string Origine { get; set; }

        public string Confezionamento { get; set; }

        public string Peso { get; set; }
        [Display(Name = "Foto 1")]
        public string FotoProdotto1 { get; set; }
        [Display(Name = "Foto 2")]
        public string FotoProdotto2 { get; set; }
        [Display(Name = "Foto 3")]
        public string FotoProdotto3 { get; set; }

        [Column(TypeName = "money")]
        public decimal Prezzo { get; set; }
        [Display(Name = "Azienda")]
        public int IdAzienda { get; set; }
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        public virtual Aziende Aziende { get; set; }

        public virtual Categorie Categorie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dettaglio_Ordine> Dettaglio_Ordine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scaffale_Prodotti> Scaffale_Prodotti { get; set; }
    }
}
