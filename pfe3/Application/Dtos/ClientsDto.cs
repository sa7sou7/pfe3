using pfe3.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace pfe3.Application.Dtos
{
    public class ClientsDto
    {
    
        
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

            public List<ContactsDto> Contactes { get; set; } = new List<ContactsDto>();
        }
    }


