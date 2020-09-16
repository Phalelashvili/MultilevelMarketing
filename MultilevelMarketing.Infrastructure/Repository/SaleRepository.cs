using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using MultilevelMarketing.Domain.Interfaces;
using MultilevelMarketing.Domain.Models;
using MultilevelMarketing.Infrastructure.Context;

namespace MultilevelMarketing.Infrastructure.Repository
{
    public class SaleRepository : ISaleRepository
    {
        protected readonly MlmContext Db;
        protected readonly DbSet<Sale> DbSet;

        public SaleRepository(MlmContext db)
        {
            Db = db;
            DbSet = Db.Set<Sale>();
            
        }

        public Sale Add(Sale sale)
        {
            DbSet.Add(sale);
            Db.SaveChanges();
            return sale;
        }

        public Sale Update(Sale sale)
        {
            DbSet.Update(sale);
            Db.SaveChanges();
            return sale;
        }

        public void UpdateRange(IEnumerable<Sale> sales)
        {
            DbSet.UpdateRange(sales);
            Db.SaveChanges();
        }
        
        public Sale Delete(Sale sale)
        {
            DbSet.Remove(sale);
            Db.SaveChanges();
            return sale;
        }

        public IEnumerable<Sale> GetAll()
        {
            return DbSet
                .Include(d => d.Distributor)
                .Include(d => d.Product)
                .OrderByDescending(s => s.Id)
                .ToList();
        }

        public IEnumerable<Sale> Filter(int? distributorId, int? productId, DateTime? startDate, DateTime? endDate)
        {
            var command = new List<string>();
            var parameters = new List<object>(4);

            var index = 0;
            
            if (distributorId != null)
            {
                command.Add($"DistributorId == @{index++}");
                parameters.Add(distributorId);
            }

            if (productId != null)
            {
                command.Add($"ProductId == @{index++}");
                parameters.Add(productId);
            }

            if (startDate != null)
            {
                command.Add($"Date >= @{index++}");
                parameters.Add(startDate);
            }

            if (endDate != null)
            {
                command.Add($"Date <= @{index++}");
                parameters.Add(endDate);
            }
            
            return DbSet
                .Include(d => d.Distributor)
                .Include(d => d.Product)
                .Where(string.Join(" AND ", command), parameters.ToArray())
                .OrderByDescending(s => s.Id)
                .ToList();
        }

        public Sale? GetById(int id)
        {
            return DbSet
                .Include(d => d.Distributor)
                .Include(d => d.Product)
                .AsNoTracking()
                .FirstOrDefault(d => d.Id == id);

        }

        public IEnumerable<Sale> GetSalesBetween(int id, DateTime startDate, DateTime endDate)
        {
            return from sale in DbSet.ToList()
                where sale.DistributorId != null // deleted distributors
                where sale.DistributorId == id
                where sale.Date >= startDate
                where sale.Date <= endDate
                select sale;
        }
    }
}