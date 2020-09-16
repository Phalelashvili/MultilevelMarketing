using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MultilevelMarketing.Domain.Interfaces;
using MultilevelMarketing.Domain.Models;
using MultilevelMarketing.Infrastructure.Context;

namespace MultilevelMarketing.Infrastructure.Repository
{
    public class DistributorRepository : IDistributorRepository
    {
        protected readonly MlmContext Db;
        
        protected readonly DbSet<Distributor> DbSet;

       
        public DistributorRepository(MlmContext db)
        {
            Db = db;
            DbSet = Db.Set<Distributor>();
        }
        
        public Distributor Add(Distributor distributor)
        {
            DbSet.Add(distributor);
            Db.SaveChanges();
            return distributor;
        }

        public Distributor Update(Distributor distributor)
        {
            DbSet.Update(distributor);
            Db.SaveChanges();
            return distributor;
        }

        public Distributor Delete(Distributor distributor)
        {
            DbSet.Remove(distributor);
            Db.SaveChanges();
            return distributor;
        }

        public IEnumerable<Distributor> GetAll()
        {
            return DbSet
                .Include(d => d.Document)
                .Include(d => d.Address)
                .Include(d => d.Contact)
                .Include(d => d.Referrer)
                .OrderByDescending(d => d.Id)
                .ToList();
        }

        public Distributor? GetById(int id)
        {
            return DbSet
                .Include(d => d.Document)
                .Include(d => d.Address)
                .Include(d => d.Contact)
                .Include(d => d.Referrer)
                .AsNoTracking()
                .SingleOrDefault(d => d.Id == id);
        }

        public IEnumerable<Distributor>[] GetDescendantReferrers(Distributor distributor, int hierarchyLevel)
        {
            var referredDistributors = new IEnumerable<Distributor>[hierarchyLevel];
            
            // first level
            referredDistributors[0] = DbSet.Where(d => d.Referrer == distributor).ToList();
            
            for (int i = 1; i < hierarchyLevel; i++)
            {
                var previousLayerReferrals = new List<Distributor>();
                foreach (var previousLayer in referredDistributors[i - 1])
                {
                    previousLayerReferrals.AddRange(DbSet.Where(d => d.Referrer == previousLayer).ToList());
                }
                referredDistributors[i] = previousLayerReferrals;
            }
            
            return referredDistributors;
        }

        public IEnumerable<Distributor> GetAncestorReferrers(Distributor distributor)
        {
            var ancestors = new List<Distributor>();

            var current = distributor;
            while (true)
            {
                if (current.Referrer == null) break;
                current = GetById(current.Referrer.Id);
                if (current == null) break;
                
                ancestors.Add(current);
            }
            
            return ancestors;
        }
    }
}