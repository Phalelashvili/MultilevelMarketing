using System;
using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Application.Input
{
    public class FilterSalesRequest
    {
        public int? DistributorId { get; set; }
        
        public int? ProductId { get; set; }
        
        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }

    }
}