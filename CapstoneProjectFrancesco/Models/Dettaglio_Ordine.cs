namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dettaglio_Ordine
    {
        [Key]
        public int IdDettaglioOrdine { get; set; }
        [Display(Name = "Quantità")]
        public int Quantita { get; set; }
        [Display(Name = "Prodotto")]
        public int IdProdotto { get; set; }
        [Display(Name = "Ordine n.")]
        public int IdOrdine { get; set; }

        public virtual Ordine Ordine { get; set; }

        public virtual Prodotti Prodotti { get; set; }
        public Dettaglio_Ordine() { }
        public Dettaglio_Ordine(int quantita, int idProdotto, int idOrdine)
        {
            Quantita = quantita;
            IdProdotto = idProdotto;
            IdOrdine = idOrdine;
        }
    }
}
