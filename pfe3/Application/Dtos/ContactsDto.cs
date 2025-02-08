using System.ComponentModel.DataAnnotations;

namespace pfe3.Application.Dtos
{
    public class ContactsDto
    {

       

    
        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}

