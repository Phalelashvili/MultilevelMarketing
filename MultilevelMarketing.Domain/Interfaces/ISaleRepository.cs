using System;
using System.Collections.Generic;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Domain.Interfaces
{
    public interface ISaleRepository
    {
        public Sale Add(Sale distributor);

        public Sale Update(Sale distributor);

        public void UpdateRange(IEnumerable<Sale> sales);

        public Sale Delete(Sale distributor);
        
        public IEnumerable<Sale> GetAll();

        public IEnumerable<Sale> Filter(int? distributorId, int? productId, DateTime? startDate, DateTime? endDate);
        
        public Sale? GetById(int id);

        public IEnumerable<Sale> GetSalesBetween(int id, DateTime startDate, DateTime endDate);
    }
}