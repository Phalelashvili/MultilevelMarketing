using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MultilevelMarketing.Domain.Models
{
    public class Product
    {
        public Product(string code, 
                        string name,
                        decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }
        
        // empty constructor for EF
        protected Product() { }
        
        public int Id { get; set; }
        
        [Required, MaxLength(10)]
        public string Code { get; set; }
        
        [Required, MaxLength(100)]
        public string Name { get; set; }
    
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The {0} field must be greater than {1}.")]
        public decimal Price { get; set; }
    }
}