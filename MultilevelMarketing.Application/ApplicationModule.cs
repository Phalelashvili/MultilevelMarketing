using Autofac;
using MultilevelMarketing.Application.Interfaces;
using MultilevelMarketing.Application.Services;

namespace MultilevelMarketing.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DistributorService>().As(typeof(IDistributorService));
            builder.RegisterType<ProductService>().As(typeof(IProductService));
            builder.RegisterType<SaleService>().As(typeof(ISaleService));
            builder.RegisterType<BonusCalculatorService>().As(typeof(IBonusCalculatorService));
        }
    }
}