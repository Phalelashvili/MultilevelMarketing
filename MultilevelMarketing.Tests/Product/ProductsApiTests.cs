using System.Net.Http;
using MultilevelMarketing.Application.Output;
using Xunit;
using Xunit.Abstractions;

namespace MultilevelMarketing.Tests.Product
{
    public class ProductsApiTests : IntegrationTestBase
    {
        public ProductsApiTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async void DeleteProductTest()
        {
            var product = (await RegisterDummyProduct()).Product;

            await MakeDeleteRequest($"api/products/{product.Id}")
                .Content.ReadAsAsync<GeneralResponse>();

            var response = await MakeGetRequest($"api/products/{product.Id}")
                .Content.ReadAsAsync<GeneralResponse>();
            
            Assert.False(response.Success);
        }
        
        [Fact]
        public async void AddSaleTest()
        {
            var request = new
            {
                Name = "Flex Tape",
                Code = "ABC234",
                Price = (decimal) 12.99
            };
            
            var sale = (await MakePostRequest("api/products", request)
                .Content.ReadAsAsync<GeneralResponse>()).Product;

            var actual = (await MakeGetRequest($"api/products/{sale.Id}")
                .Content.ReadAsAsync<GeneralResponse>()).Product;
            
            Assert.Equal(actual.Name, request.Name);
            Assert.Equal(actual.Code, request.Code);
            Assert.Equal(actual.Price, request.Price);
        }
        
    }
}