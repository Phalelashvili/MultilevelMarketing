using System.Collections.Generic;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Domain.Interfaces
{
    public interface IDistributorRepository
    {
        public Distributor Add(Distributor distributor);

        public Distributor Update(Distributor distributor);

        public Distributor Delete(Distributor distributor);
        
        public IEnumerable<Distributor> GetAll();

        public Distributor? GetById(int id);

        public IEnumerable<Distributor>[] GetDescendantReferrers(Distributor distributor, int hierarchyLevel);
        public IEnumerable<Distributor> GetAncestorReferrers(Distributor distributor);
    }
}