namespace CapstoneProjectFrancesco.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Richeste_Clienti
    {
        [Key]
        public int IdRichiesta { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }

        public string Email { get; set; }
        [Display(Name = "Numero telefono")]
        public string NumeroTelefono { get; set; }
        [Display(Name = "Richiesta")]
        public string MessaggioRichiesta { get; set; }
    }
}
