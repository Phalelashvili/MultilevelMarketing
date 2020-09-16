using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Application.Input.UpdateRequests
{
    public class UpdateProductRequest : AddProductRequest
    {
        [Required]
        public int? Id { get; set; }
    }
}