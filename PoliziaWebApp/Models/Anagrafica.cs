using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PoliziaWebApp.Models
{
    public class Anagrafica
    {
        public int IdAnagrafica { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        [Required]
        public string Città { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "CAP must be exactly 5 digits.")]
        public string CAP { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{16}$", ErrorMessage = "Cod_Fisc must be exactly 16 alphanumeric characters.")]
        public string Cod_Fisc { get; set; }
    }
}
