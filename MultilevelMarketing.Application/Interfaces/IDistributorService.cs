using System.Collections.Generic;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Input.UpdateRequests;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Application.Interfaces
{
    public interface IDistributorService
    {
        public GeneralResponse Register(RegisterDistributorRequest request);

        public GeneralResponse Update(UpdateDistributorRequest request);

        public GeneralResponse Delete(int id);

        public GeneralResponse GetById(int id);
        
        public IEnumerable<Distributor> GetAll();
    }
}