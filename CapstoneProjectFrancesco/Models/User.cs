namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Ordine = new HashSet<Ordine>();
        }

        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }

        [Required]
        [StringLength(80)]
        [Remote("IsEmailValid","Validazioni",ErrorMessage = "Indirizzo email già presente")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Remote("IsPasswordValid", "Validazioni", ErrorMessage = "Password già presente. Registrare una nuova password")]
        public string Password { get; set; }

        [StringLength(50)]
        public string Ruolo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordine> Ordine { get; set; }
    }
}
