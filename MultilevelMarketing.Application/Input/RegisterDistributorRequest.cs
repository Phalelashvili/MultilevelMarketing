using System;
using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Application.Input
{
    public class RegisterDistributorRequest
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }
        
        [Required]
        public int Sex { get; set; }
        
        [Required]
        public byte[] Picture { get; set; }
        
        [Required]
        public int DocumentType { get; set; }

        [MaxLength(10)]
        public string DocumentSerialCode { get; set; }
        
        [MaxLength(10)]
        public string DocumentNumber { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DocumentIssueDate { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DocumentExpireDate { get; set; }

        [MaxLength(100)]
        public string DocumentIssuingAuthority { get; set; }
        
        [Required, MaxLength(50)]
        public string PersonalNumber { get; set; }

        [Required]
        public int ContactType { get; set; }
        
        [Required, MaxLength(100)]
        public string ContactDetails { get; set; }
        
        [Required]
        public int AddressType { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }
        
        public int? Referrer { get; set; }
    }
}