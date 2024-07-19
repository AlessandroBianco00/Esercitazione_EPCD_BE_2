using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PoliziaWebApp.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }

        [Required]
        public DateTime DataViolazione { get; set; }

        [Required]
        public string IndirizzoViolazione { get; set; }

        [Required]
        public string Nominativo_Agente { get; set; }

        [Required]
        public DateTime DataTrascrizioneVerbale { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Importo must be greater than 0.")]
        public decimal Importo { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "DecurtamentoPunti cannot be negative.")]
        public int DecurtamentoPunti { get; set; }

        [Required]
        public int IdAnagrafico_FK { get; set; }

        [Required]
        public int IdViolazione_FK { get; set; }
    }
}
