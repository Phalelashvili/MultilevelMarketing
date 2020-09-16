using System;
using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Application.Input
{
    public class BonusCalculatorRequest
    {
        [Required]
        public int? Distributor { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
    }
}