using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Domain.Models
{
    public class ContactInfo
    {
        public ContactInfo(int contactType, string contactDetails)
        {
            ContactType = contactType;
            ContactDetails = contactDetails;
        }
        
        // empty constructor for EF
        protected ContactInfo() { }
        
        public int Id { get; set; }

        [Required]
        public int ContactType { get; set; }
        
        [Required, MaxLength(100)]
        public string ContactDetails { get; set; }

    }
}