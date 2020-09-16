using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MultilevelMarketing.Domain.Interfaces;
using MultilevelMarketing.Domain.Models;
using MultilevelMarketing.Infrastructure.Context;

namespace MultilevelMarketing.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        protected readonly MlmContext Db;
        protected readonly DbSet<Product> DbSet;

        public ProductRepository(MlmContext db)
        {
            Db = db;
            DbSet = Db.Set<Product>();
        }

        public Product Add(Product product)
        {
            DbSet.Add(product);
            Db.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            DbSet.Update(product);
            Db.SaveChanges();
            return product;
        }

        public Product Delete(Product product)
        {
            DbSet.Remove(product);
            Db.SaveChanges();
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return DbSet
                .OrderByDescending(p => p.Id)
                .ToList();
        }

        public Product GetById(int id)
        {
            return DbSet.Find(id);
        }
    }
}