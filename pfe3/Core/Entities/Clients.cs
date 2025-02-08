using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pfe3.Core.Entities
{
    
    
        public enum StatutProspect { Nouveau, EnCours, Converti, Abandonne }

        public class Clients
        {
            [Key]
            public int ClientId { get; set; }

            [Required]
            public string Nom { get; set; }

            [Required]
            public string Prenom { get; set; }

            [Required]
            public string Adresse { get; set; }

            [Required]
            public string Cin { get; set; }

            [Required]
            public string Matricule { get; set; }

            // ✅ Ajout du statut directement dans Clients
            public StatutProspect Statut { get; set; } = StatutProspect.Nouveau;

            // Relations
            [JsonIgnore]
            public ICollection<Contactes> Contactes { get; set; } = new List<Contactes>();
        }
    }

