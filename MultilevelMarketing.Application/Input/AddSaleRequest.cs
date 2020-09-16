using System;
using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Application.Input
{
    public class AddSaleRequest
    {
        [Required]
        public int? Distributor { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }
        
        [Required]
        public int? Product { get; set; }
        
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The {0} field must be greater than {1}.")]
        public decimal? Price { get; set; }
        
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The {0} field must be greater than {1}.")]
        public decimal? SeparatePrice { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The {0} field must be greater than {1}.")]
        public decimal? TotalPrice { get; set; }
    }
}