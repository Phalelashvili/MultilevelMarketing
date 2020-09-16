using System;
using System.Net.Http;
using System.Threading.Tasks;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Configs;
using Xunit;
using Xunit.Abstractions;

namespace MultilevelMarketing.Tests
{
    public class ReferralHierarchyIntegrationTests : IntegrationTestBase
    {
        public ReferralHierarchyIntegrationTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }
        
        [Fact]
        public async void FirstLevelReferrals_ShouldNotExceedLimit()
        {
            // create root distributor
            var response = await RegisterDistributor();
            
            var rootDistributor = response.Distributor;
            
            // create referrals with root distributor id
            for (int i = 0; i < ReferralConfig.FirstLevelReferralLimit; i++)
            {
                response = await RegisterDistributor(referrer: rootDistributor.Id);
                
                Assert.True(response.Success);
            }
            
            // limit reached, registration should fail
            response = await RegisterDistributor(referrer: rootDistributor.Id);

            Assert.False(response.Success);
        }

        [Fact]
        public async void ReferralLayer_ShouldNotExceedLimit()
        {
            // create root distributor with no referral
            var response = await RegisterDistributor();
            var currentId = response.Distributor.Id;

            for (int i = 0; i < ReferralConfig.ReferralLayerLimit; i++)
            {
                response = await RegisterDistributor(referrer: currentId);
                Assert.True(response.Success);
                currentId = response.Distributor.Id;
            }
            
            // limit reached, registration should fail
            response = await RegisterDistributor(currentId);
            
            Assert.False(response.Success);
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
                .Content.ReadAsAsync<GeneralResponse>();

        }
    }
}