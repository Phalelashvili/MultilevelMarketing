using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultilevelMarketing.Api;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Infrastructure.Context;
using Xunit.Abstractions;

namespace MultilevelMarketing.Tests
{
    public class IntegrationTestBase
    {
        protected readonly ITestOutputHelper _testOutputHelper;
        protected readonly HttpClient _client;
        
        protected IntegrationTestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder => // replace real db with in-memory one
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                 typeof(DbContextOptions<MlmContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        // Add ApplicationDbContext using an in-memory database for testing.
                        services.AddDbContext<MlmContext>(options =>
                        {
                            options.UseInMemoryDatabase("testDb");
                        });
                        // Build the service provider.
                        services.BuildServiceProvider();
                    });
                });
            
            _client = appFactory.CreateClient();
        }

        protected HttpResponseMessage MakeGetRequest(string url)
        {
            return _client.GetAsync(url).Result;
        }

        protected HttpResponseMessage MakeDeleteRequest(string url)
        {
            return _client.DeleteAsync(url).Result;
        }

        protected HttpResponseMessage MakePostRequest<T>(string url, T body)
        {
            return _client.PostAsJsonAsync(url, body).Result;
        }
        
        protected HttpResponseMessage MakePutRequest<T>(string url, T body)
        {
            return _client.PutAsJsonAsync(url, body).Result;
        }

        protected async Task<GeneralResponse> RegisterDummySale(int distributorId, int productId)
        {
            var request = new AddSaleRequest()
            {
                Distributor = distributorId,
                Product = productId,
                Price = (decimal) 99.99,
                SeparatePrice = (decimal) 99.99,
                TotalPrice = (decimal) 99.99 * 10,
                Date = DateTime.Now,
            };
            
            return await MakePostRequest("api/sales", request)
                .Content.ReadAsAsync<GeneralResponse>();
        }

        protected async Task<GeneralResponse> RegisterDummyProduct()
        {
            var request = new AddProductRequest()
            {
                Name = "Flex Tape",
                Code = "ABC123",
                Price = (decimal) 99.99,
            };

            return await MakePostRequest("api/products", request)
                .Content.ReadAsAsync<GeneralResponse>();
        }

        protected async Task<GeneralResponse> RegisterDummyDistributor(int? referrer = null)
        {
            var request = new RegisterDistributorRequest()
            {
                FirstName = "Ken",
                LastName = "Kaneff",
                BirthDate = DateTime.Now,
                Sex = 0,
                Picture = new byte[] { 0xFF },
                DocumentType = 0,
                DocumentSerialCode = "0123",
                DocumentNumber = "4566",
                DocumentIssueDate = DateTime.Now,
                DocumentExpireDate = DateTime.Now,
                DocumentIssuingAuthority = "xyz",
                PersonalNumber = "100000000",
                ContactType = 0,
                ContactDetails = "email@example.com",
                AddressType = 0,
                Address = "221B Baker St.",
                Referrer = referrer
            };
            
            return await MakePostRequest("api/distributors", request)
                .Content.ReadAsAsync<GeneralResponse>();
        }
    }
}