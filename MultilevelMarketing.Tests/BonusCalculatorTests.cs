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
using MultilevelMarketing.Domain.Configs;
using MultilevelMarketing.Infrastructure.Context;
using Xunit;
using Xunit.Abstractions;

namespace MultilevelMarketing.Tests
{
    public class BonusCalculatorTests : IntegrationTestBase
    {
        public BonusCalculatorTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }
        
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 0, 1000)]
        [InlineData(0, 200, 2500)]
        [InlineData(800, 500, 3000)]
        public async void BonusCalculatorTest(int selfMadeSales, int layerOneSales, int layerTwoSales)
        {
            // add dummy product
            await AddProduct("ABC123", "Flex Tape", (decimal) 12.99);
            
            // register root (target) and referred distributors
            var rootDistributor = (await RegisterDistributor()).Distributor;
            var levelOneReferrer = (await RegisterDistributor(rootDistributor.Id)).Distributor;
            var levelTwoReferrer = (await RegisterDistributor(levelOneReferrer.Id)).Distributor;

            // add sales for distributors created above
            await AddSale(rootDistributor.Id, selfMadeSales);
            await AddSale(levelOneReferrer.Id, layerOneSales);
            await AddSale(levelTwoReferrer.Id, layerTwoSales);
            
            var expectedBonus =
                selfMadeSales * BonusCalculatorConfig.SelfMadeSalesBonus +
                layerOneSales * BonusCalculatorConfig.ReferredSaleBonuses[0] +
                layerTwoSales * BonusCalculatorConfig.ReferredSaleBonuses[1];

            var request = new BonusCalculatorRequest()
                {Distributor = rootDistributor.Id, StartDate = DateTime.Today, EndDate = DateTime.Now};
            
            var bonusCalculatorOutput = await MakePostRequest("api/bonuscalculator", request)
                .Content.ReadAsAsync<BonusCalculatorResponse>();
            
            Assert.Equal(expectedBonus, bonusCalculatorOutput.TotalBonus);

        }

        private async Task<GeneralResponse> AddSale(int distributor, int price)
        {
            var request = new AddSaleRequest()
            {
                Distributor = distributor,
                Price = price,
                TotalPrice = price,
                Date = DateTime.Now,
                Product = 1,
                SeparatePrice = price,
            };
            
            return await MakePostRequest("api/sales", request)
                .Content.ReadAsAsync<GeneralResponse>();
        }

        private async Task<GeneralResponse> AddProduct(string code, string name, decimal price)
        {
            var request = new AddProductRequest()
            {
                Code = code,
                Name = name,
                Price = price,
            };
            
            return await MakePostRequest("api/products", request)
                .Content.ReadAsAsync<GeneralResponse>();
        }

        
        private async Task<GeneralResponse> RegisterDistributor(int? referrer = null)
        {
            var request = new RegisterDistributorRequest()
            {
                FirstName = "Ken",
                LastName = "Kaneff",
                BirthDate = DateTime.Now,
                Sex = 0,
                Picture = new byte[] { },
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
                .Content.ReadAsAsync<GeneralResponse>();;

        }

    }
}