using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Application.Input.UpdateRequests
{
    public class UpdateSaleRequest : AddSaleRequest
    {
        [Required]
        public int? Id { get; set; }
    }
}