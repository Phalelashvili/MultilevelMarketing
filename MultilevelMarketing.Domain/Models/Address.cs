using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace MultilevelMarketing.Domain.Models
{
    public class Address
    {
        public Address(int addressType, string addressDetails)
        {
            AddressType = addressType;
            AddressDetails = addressDetails;
        }
        
        // empty constructor for EF
        protected Address() { }

        public int Id { get; set; }

        [Required]
        public int AddressType { get; set; }

        [Required]
        public string AddressDetails { get; set; }

    }
}