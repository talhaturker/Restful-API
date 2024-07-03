using System.ComponentModel.DataAnnotations;

namespace MyRestApi.Models
{
    public class SparePart
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public double Price { get; set; }
    }
}
