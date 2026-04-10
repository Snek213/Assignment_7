using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_7.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string? PictureUri { get; set; }

        [Required]
        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string? Genre { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Range(1, 100)]
        public int Rating { get; set; } 
    }
}
