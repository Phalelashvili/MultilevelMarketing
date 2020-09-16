using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Application.Input.UpdateRequests
{
    public class UpdateDistributorRequest : RegisterDistributorRequest
    {
        [Required]
        public int? Id { get; set; }

        public new byte[] Picture { get; set; } // removed required attribute
    }
}