using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Application.Input
{
    public class AddProductRequest
    {
        [Required, MaxLength(100)]
        public string? Code { get; set; }
        
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The {0} field must be greater than {1}.")]
        public decimal? Price { get; set; }
    }
}