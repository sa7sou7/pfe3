using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pfe3.Core.Entities
{
   
   
        public class Contactes
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string Phone { get; set; }

            public int ClientId { get; set; }

            [ForeignKey("ClientId")]
            public Clients Client { get; set; }
        }
    

}
