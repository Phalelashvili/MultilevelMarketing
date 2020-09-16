using System;
using System.Net.Http;
using MultilevelMarketing.Application.Output;
using Xunit;
using Xunit.Abstractions;

namespace MultilevelMarketing.Tests.Sale
{
    public class SalesApiTests : IntegrationTestBase
    {
        public SalesApiTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async void DeleteSaleTest()
        {
            var distributor = (await RegisterDummyDistributor()).Distributor;
            var product = (await RegisterDummyProduct()).Product;
            var sale = (await RegisterDummySale(distributor.Id, product.Id)).Sale;

            await MakeDeleteRequest($"api/sales/{sale.Id}")
                .Content.ReadAsAsync<GeneralResponse>();

            var response = await MakeGetRequest($"api/sales/{sale.Id}")
                .Content.ReadAsAsync<GeneralResponse>();
            
            Assert.False(response.Success);
        }
        
        [Fact]
        public async void DeletingForeignKey_ShouldNotDeleteSale()
        {
            var distributor = (await RegisterDummyDistributor()).Distributor;
            var product = (await RegisterDummyProduct()).Product;
            var sale = (await RegisterDummySale(distributor.Id, product.Id)).Sale;

            // delete distributor and product
            MakeDeleteRequest($"api/distributors/{distributor.Id}");
            MakeDeleteRequest($"api/products/{product.Id}");

            // check that sale still exists
            var response = await MakeGetRequest($"api/sales/{sale.Id}")
                .Content.ReadAsAsync<GeneralResponse>();
            
            Assert.True(response.Success);
        }

        [Fact]
        public async void AddSaleTest()
        {
            var distributor = (await RegisterDummyDistributor()).Distributor;
            var product = (await RegisterDummyProduct()).Product;

            var request = new
            {
                Distributor = distributor.Id,
                Product = product.Id,
                Price = (decimal) 99.99,
                SeparatePrice = (decimal) 99.99,
                TotalPrice = (decimal) 99.99 * 10,
                Date = DateTime.Now,
            };
            
            var sale = (await MakePostRequest("api/sales", request)
                .Content.ReadAsAsync<GeneralResponse>()).Sale;

            var actual = (await MakeGetRequest($"api/sales/{sale.Id}")
                .Content.ReadAsAsync<GeneralResponse>()).Sale;
            
            Assert.Equal(actual.DistributorId, request.Distributor);
            Assert.Equal(actual.ProductId, request.Product);
            Assert.Equal(actual.Price, request.Price);
            Assert.Equal(actual.SeparatePrice, request.SeparatePrice);
            Assert.Equal(actual.TotalPrice, request.TotalPrice);
            Assert.Equal(actual.Date, request.Date);
        }
        
    }
}