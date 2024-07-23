using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Dto
{
    public class CustomerDto
    {
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il Codice Fiscale deve essere lungo 16 caratteri.")]
        public string FiscalCode { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Il numero di telefono deve contenere 10 caratteri.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Phone]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Il numero di cellulare deve contenere 10 caratteri.")]
        public string MobilePhoneNumber { get; set; }
    }
}
