using System;
using System.ComponentModel.DataAnnotations;

namespace MultilevelMarketing.Domain.Models
{
    public class Document
    {
        public Document(int documentType, string documentSerialCode, string documentNumber, string personalNumber,
            string? documentIssuingAuthority, DateTime? documentIssue = null, DateTime? documentExpireDate = null)
        {
            DocumentType = documentType;
            DocumentSerialCode = documentSerialCode;
            DocumentNumber = documentNumber;
            DocumentIssueDate = documentIssue;
            DocumentExpireDate = documentExpireDate;
            DocumentIssuingAuthority = documentIssuingAuthority;
            PersonalNumber = personalNumber;
        }
        
        // empty constructor for EF
        protected Document() { }
        
        public int Id { get; set; }

        [Required]
        public int DocumentType { get; set; }

        [MaxLength(10)]
        public string DocumentSerialCode { get; set; }
        
        [MaxLength(10)]
        public string DocumentNumber { get; set; }
        
        [Required]
        public DateTime? DocumentIssueDate { get; set; }

        [Required]
        public DateTime? DocumentExpireDate { get; set; }

        [MaxLength(100)]
        public string? DocumentIssuingAuthority { get; set; }
        
        [Required, MaxLength(50)]
        public string PersonalNumber { get; set; }
    }
}