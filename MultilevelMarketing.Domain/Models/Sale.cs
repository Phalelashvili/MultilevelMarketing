using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultilevelMarketing.Domain.Models
{
    public class Sale
    {
        public Sale(int distributorId,
                    int productId,
                    DateTime date,
                    decimal price,
                    decimal separatePrice,
                    decimal totalPrice)
        {
            DistributorId = distributorId;
            ProductId = productId;
            Date = date;
            Price = price;
            SeparatePrice = separatePrice;
            TotalPrice = totalPrice;
        }
        
        // empty constructor for EF
        protected Sale() { }
        
        public int Id { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal SeparatePrice { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [DefaultValue(false)]
        public bool BonusCollected { get; set; }
        
        [ForeignKey("DistributorId")]
        public int? DistributorId { get; set; }
        public Distributor? Distributor { get; set; }

        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}