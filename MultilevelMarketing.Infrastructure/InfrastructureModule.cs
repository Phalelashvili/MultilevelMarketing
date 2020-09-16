using Autofac;
using MultilevelMarketing.Domain.Interfaces;
using MultilevelMarketing.Infrastructure.Repository;

namespace MultilevelMarketing.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DistributorRepository>().As(typeof(IDistributorRepository));
            builder.RegisterType<ProductRepository>().As(typeof(IProductRepository));
            builder.RegisterType<SaleRepository>().As(typeof(ISaleRepository));
        }
    }
}