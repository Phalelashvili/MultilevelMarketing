using System.Collections.Generic;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Domain.Interfaces
{
    public interface IProductRepository
    {
        public Product Add(Product distributor);

        public Product Update(Product distributor);

        public Product Delete(Product distributor);
        
        public IEnumerable<Product> GetAll();
        
        public Product GetById(int id);
    }
}