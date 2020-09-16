using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MultilevelMarketing.Domain.Models
{
    public class Distributor
    {
        public Distributor(string firstName,
                            string lastName,
                            DateTime? birthDate,
                            int sex,
                            byte[] picture,
                            Document document,
                            ContactInfo contact,
                            Address address,
                            int? referrerId = null)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Sex = sex;
            Picture = picture;
            Document = document;
            Contact = contact;
            Address = address;
            ReferrerId = referrerId;
        }
        
        // empty constructor for EF
        protected Distributor() { }
        
        public int Id { get; set; }
        
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
        public Document Document { get; set; }

        [Required]
        public ContactInfo Contact { get; set; }
        
        [Required]
        public Address Address { get; set; }
        
        public int? ReferrerId { get; set; }
        
        [ForeignKey("ReferrerId")]
        public Distributor? Referrer { get; set; }
    }
}