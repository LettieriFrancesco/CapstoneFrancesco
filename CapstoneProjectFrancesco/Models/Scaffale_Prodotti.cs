namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Scaffale_Prodotti
    {
        [Key]
        public int IdScaffaleProdotti { get; set; }
        [Display(Name = "Scaffale")]
        public int IdScaffale { get; set; }
        [Display(Name = "Prodotto")]
        public int IdProdotto { get; set; }
        [NotMapped]
        public string NomeScaffale { get; set; }
        [NotMapped]
        public string NomeProdottoScaffale { get; set; }
        [NotMapped]
        public string Corsia {  get; set; }

        public virtual Prodotti Prodotti { get; set; }

        public virtual Scaffale_Magazzino Scaffale_Magazzino { get; set; }
    }
}
