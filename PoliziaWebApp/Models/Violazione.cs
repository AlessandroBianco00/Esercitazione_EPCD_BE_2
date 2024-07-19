using System.ComponentModel.DataAnnotations;

namespace PoliziaWebApp.Models
{
    public class Violazione
    {
        public int IdViolazione { get; set; }

        [Required]
        public string Descrizione { get; set; }
    }
}
