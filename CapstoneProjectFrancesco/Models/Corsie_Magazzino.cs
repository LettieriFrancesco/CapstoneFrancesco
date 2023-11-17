namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Corsie_Magazzino
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Corsie_Magazzino()
        {
            Scaffale_Magazzino = new HashSet<Scaffale_Magazzino>();
        }

        [Key]
        public int IdCorsia { get; set; }

        [Required]
        [Display(Name = "Descrizione corsia")]
        public string DescrizioneCorsia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scaffale_Magazzino> Scaffale_Magazzino { get; set; }
    }
}
