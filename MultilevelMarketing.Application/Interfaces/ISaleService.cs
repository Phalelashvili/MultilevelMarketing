using System.Collections.Generic;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Input.UpdateRequests;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Application.Interfaces
{
    public interface ISaleService
    {
        public GeneralResponse Add(AddSaleRequest addSaleRequest);

        public GeneralResponse Update(UpdateSaleRequest updateSaleRequest);

        public GeneralResponse Delete(int id);

        public GeneralResponse GetById(int id);

        public IEnumerable<Sale> Filter(FilterSalesRequest filterSalesRequest);
        
        public IEnumerable<Sale> GetAll();
    }
}