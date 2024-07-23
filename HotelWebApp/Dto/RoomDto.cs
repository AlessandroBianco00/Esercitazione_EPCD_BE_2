using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Dto
{
    public enum RoomType
    {
        Single = '1',
        Double = '2'
    }

    public class RoomDto
    {
        public int RoomNumber { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public RoomType Type { get; set; }
    }
}
