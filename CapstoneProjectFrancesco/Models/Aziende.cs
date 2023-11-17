namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Aziende")]
    public partial class Aziende
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aziende()
        {
            Prodotti = new HashSet<Prodotti>();
        }

        [Key]
        public int IdAzienda { get; set; }

        [Required]
        [Display(Name = "Nome Azienda")]
        public string NomeAzienda { get; set; }
        [Display(Name = "Indirizzo Azienda")]
        public string IndirizzoAzienda { get; set; }
        [Display(Name = "Recapito Azienda")]
        public string RecapitoAzienda { get; set; }
        [Display(Name = "Descrizione Azienda")]
        public string DescrizioneAzienda { get; set; }
        [Display(Name = "Foto")]
        public string FotoAzienda { get; set; }
        [Display(Name = "Logo Azienda")]
        public string LogoAzienda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prodotti> Prodotti { get; set; }
    }
}
