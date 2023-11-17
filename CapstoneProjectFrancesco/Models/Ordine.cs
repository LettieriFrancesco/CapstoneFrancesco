namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordine")]
    public partial class Ordine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordine()
        {
            Dettaglio_Ordine = new HashSet<Dettaglio_Ordine>();
        }

        [Key]
        public int IdOrdine { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }
        public string StatoConsegna { get; set; }

        public string StatoOrdine { get; set; }

        public string Indirizzo { get; set; }

        [Column(TypeName = "money")]
        public decimal Importo { get; set; }

        public bool? Spedizione { get; set; }

        public int? ImportoSpedizione { get; set; }
        [Display(Name = "User")]
        public int IdUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dettaglio_Ordine> Dettaglio_Ordine { get; set; }

        public virtual User User { get; set; }
    }
}
