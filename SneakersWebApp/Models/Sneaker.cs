using System.ComponentModel.DataAnnotations;

namespace SneakersWebApp.Models
{
    public class Sneaker
    {
        [Required(ErrorMessage = "Inserire il nome")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Inserire descrizione")]
        public string ProductDescription { get; set; }
        public int ProductId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Il prezzo deve essere un numero positivo")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Inserire l'immagine di copertina del prodotto")]
        public IFormFile ProductCover { get; set; }
        [Required(ErrorMessage = "Inserire l'immagine del prodotto")]
        public IFormFile ProductImage1 { get; set; }
        [Required(ErrorMessage = "Inserire l'immagine del prodotto")]
        public IFormFile ProductImage2 { get; set; }

    }
}
