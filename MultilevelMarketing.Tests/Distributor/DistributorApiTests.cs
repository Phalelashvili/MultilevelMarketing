using System;
using System.Net;
using System.Net.Http;
using MultilevelMarketing.Application.Output;
using Xunit;
using Xunit.Abstractions;

namespace MultilevelMarketing.Tests.Distributor
{
    public class DistributorApiTests : IntegrationTestBase
    {
        public DistributorApiTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async void DeleteDistributorTest()
        {
            var distributor = (await RegisterDummyDistributor()).Distributor;

            await MakeDeleteRequest($"api/distributors/{distributor.Id}")
                .Content.ReadAsAsync<GeneralResponse>();

            var response = await MakeGetRequest($"api/distributors/{distributor.Id}")
                .Content.ReadAsAsync<GeneralResponse>();

            Assert.False(response.Success);
        }
        
        [Fact]
        public async void DeletingForeignKey_ShouldNotDeleteParent()
        {
            var rootDistributor = (await RegisterDummyDistributor()).Distributor;
            var refereeDistributor = (await RegisterDummyDistributor(rootDistributor.Id)).Distributor;

            // delete root distributor
            MakeDeleteRequest($"api/distributors/{rootDistributor.Id}");

            // check that referee still exists
            var response = await MakeGetRequest($"api/distributors/{refereeDistributor.Id}")
                .Content.ReadAsAsync<GeneralResponse>();
            
            Assert.True(response.Success);
        }

        [Theory] // well this looks awful
        [InlineData(HttpStatusCode.OK,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",1,"AB")] // isn't missing anything
        [InlineData(HttpStatusCode.OK,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,null,"AA","AA","AA",1,"AB",1,"AB")] // non-optional documentSerialCode
        [InlineData(HttpStatusCode.OK,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA",null,"AA","AA",1,"AB",1,"AB")] // non-optional documentNumber
        [InlineData(HttpStatusCode.OK,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA",null,"AA",1,"AB",1,"AB")] // documentIssuingAuthority
        [InlineData(HttpStatusCode.BadRequest,null,"LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",1,"AB")] // firstName
        [InlineData(HttpStatusCode.BadRequest,"FN",null,"2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",1,"AB")] // lastName
        [InlineData(HttpStatusCode.BadRequest,"FN","LN",null,"2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",1,"AB")] // birthDateStr
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01",null,"2020-01-01",null,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",1,"AB")] // documentIssueDateStr
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01",null,null,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",1,"AB")] // documentExpireDateStr
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01","2020-01-01",null,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",1,"AB")] // sex
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,null,1,"AA","AA","AA","AA",1,"AB",1,"AB")] // picture
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},null,"AA","AA","AA","AA",1,"AB",1,"AB")] // documentType
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA",null,1,"AB",1,"AB")] // personalNumber
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA","AA",null,"AB",1,"AB")] // contactType
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA","AA",1,null,1,"AB")] // contactDetails
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",null,"AB")] // addressType
        [InlineData(HttpStatusCode.BadRequest,"FN","LN","2020-01-01","2020-01-01","2020-01-01",1,new byte[]{0xFF},1,"AA","AA","AA","AA",1,"AB",1,null)] // address
        public async void RegisterTests(HttpStatusCode expectedStatusCode,
            string? firstName,
            string? lastName,
            string? birthDateStr,
            string? documentIssueDateStr,
            string? documentExpireDateStr,
            int? sex,
            byte[]? picture,
            int? documentType,
            string? documentSerialCode,
            string? documentNumber,
            string? documentIssuingAuthority,
            string? personalNumber,
            int? contactType,
            string? contactDetails,
            int? addressType,
            string? address)
        {
            DateTime? birthDate = null;
            DateTime? documentIssueDate = null;
            DateTime? documentExpireDate = null;
            
            if (birthDateStr != null)
                birthDate = DateTime.Parse(birthDateStr);
            if (documentIssueDateStr != null)
                documentIssueDate = DateTime.Parse(documentIssueDateStr);
            if (documentExpireDateStr != null)
                documentExpireDate = DateTime.Parse(documentExpireDateStr);

            var request = new
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                DocumentIssueDate = documentIssueDate,
                DocumentExpireDate = documentExpireDate,
                Sex = sex,
                Picture = picture,
                DocumentType = documentType,
                DocumentSerialCode = documentSerialCode,
                DocumentNumber = documentNumber,
                DocumentIssuingAuthority = documentIssuingAuthority,
                PersonalNumber = personalNumber,
                ContactType = contactType,
                ContactDetails = contactDetails,
                AddressType = addressType,
                Address = address,
            };

            var statusCode = MakePostRequest("api/distributors", request).StatusCode;
            
            Assert.Equal(expectedStatusCode, statusCode);

            if (statusCode == HttpStatusCode.BadRequest) return;
            
            var registerResponse = (await MakePostRequest("api/distributors", request)
                .Content.ReadAsAsync<GeneralResponse>()).Distributor;
            
            var actual = (await MakeGetRequest($"api/distributors/{registerResponse.Id}")
                .Content.ReadAsAsync<GeneralResponse>()).Distributor;

            Assert.Equal(request.FirstName, actual.FirstName);
            Assert.Equal(request.LastName, actual.LastName);
            Assert.Equal(request.BirthDate, actual.BirthDate);
            Assert.Equal(request.DocumentType, actual.Document.DocumentType);
            Assert.Equal(request.DocumentIssueDate, actual.Document.DocumentIssueDate);
            Assert.Equal(request.DocumentExpireDate, actual.Document.DocumentExpireDate);
            Assert.Equal(request.DocumentSerialCode, actual.Document.DocumentSerialCode);
            Assert.Equal(request.DocumentNumber, actual.Document.DocumentNumber);
            Assert.Equal(request.DocumentIssuingAuthority, actual.Document.DocumentIssuingAuthority);
            Assert.Equal(request.PersonalNumber, actual.Document.PersonalNumber);
            Assert.Equal(request.Sex, actual.Sex);
            Assert.Equal(request.Picture, actual.Picture);
            Assert.Equal(request.ContactType, actual.Contact.ContactType);
            Assert.Equal(request.ContactDetails, actual.Contact.ContactDetails);
            Assert.Equal(request.AddressType, actual.Address.AddressType);
            Assert.Equal(request.Address, actual.Address.AddressDetails);
        }
    }
}