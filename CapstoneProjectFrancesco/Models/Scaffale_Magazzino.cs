namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Scaffale_Magazzino
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Scaffale_Magazzino()
        {
            Scaffale_Prodotti = new HashSet<Scaffale_Prodotti>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdScaffale { get; set; }

        [Required]
        [Display(Name = "Descrizione scaffale")]
        public string DescrizioneScaffale { get; set; }
        [Display(Name = "Corsia")]
        public int IdCorsia { get; set; }

        public virtual Corsie_Magazzino Corsie_Magazzino { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scaffale_Prodotti> Scaffale_Prodotti { get; set; }
    }
}
