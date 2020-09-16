using System;
using System.Collections.Generic;
using System.Linq;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Input.UpdateRequests;
using MultilevelMarketing.Application.Interfaces;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Configs;
using MultilevelMarketing.Domain.Interfaces;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Application.Services
{
    public class DistributorService : IDistributorService
    {
        private IDistributorRepository _distributorRepository;

        public DistributorService(IDistributorRepository distributorRepository)
        {
            _distributorRepository = distributorRepository;
        }
        
        public GeneralResponse Register(RegisterDistributorRequest request)
        {
            Distributor? referrer = null;
            
            if (request.Referrer != null)
            {
                referrer = _distributorRepository.GetById((int)request.Referrer);
                
                // user specified referrer but id not found in database
                if (request.Referrer != null && referrer == null)
                {
                    return new GeneralResponse(false)
                        {Error = $"Distributor with id {request.Referrer} does not exist."};
                }

                var hierarchyLevels = _distributorRepository
                    .GetDescendantReferrers(referrer, 1);

                var parentRefferers = _distributorRepository.GetAncestorReferrers(referrer);

                if (hierarchyLevels[0].Count() >= ReferralConfig.FirstLevelReferralLimit ||
                    parentRefferers.Count() >= ReferralConfig.ReferralLayerLimit)
                    return new GeneralResponse(false) {Error = "Referrer has reached hierarchy limit."};
            }

            var address = new Address(request.AddressType, request.Address);
            var contactInfo = new ContactInfo(request.ContactType, request.ContactDetails);
            var document = new Document(
                    request.DocumentType,
                    request.DocumentSerialCode,
                    request.DocumentNumber,
                    request.PersonalNumber,
                    request.DocumentIssuingAuthority,
                    request.DocumentIssueDate,
                    request.DocumentExpireDate
            );

            
            int? referrerId = null;
            if (referrer != null)
                referrerId = referrer.Id;
            
            var distributor = new Distributor(
                request.FirstName, 
                request.LastName,
                request.BirthDate,
                request.Sex,
                request.Picture,
                document,
                contactInfo,
                address,
                referrerId
            );
            
            _distributorRepository.Add(distributor);
            return new GeneralResponse(true) {Distributor = distributor};
        }

        public IEnumerable<Distributor> GetAll()
        { 
            return _distributorRepository.GetAll();
        }

        public GeneralResponse GetById(int id)
        {
            var distributor = _distributorRepository.GetById(id);
            
            return new GeneralResponse(distributor != null)
            {
                Distributor = distributor,
                Error = distributor != null ? null : $"Distributor with id {id} does not exist."
            };
        }

        public GeneralResponse Update(UpdateDistributorRequest request)
        {
            var distributor = _distributorRepository.GetById((int) request.Id);
            
            if (distributor == null)
                return new GeneralResponse(false) {Error = $"Distributor with id {request.Id} does not exist."};
            
            var address = new Address(request.AddressType, request.Address);
            var contactInfo = new ContactInfo(request.ContactType, request.ContactDetails);
            var document = new Document(
                request.DocumentType,
                request.DocumentSerialCode,
                request.DocumentNumber,
                request.PersonalNumber,
                request.DocumentIssuingAuthority,
                request.DocumentIssueDate,
                request.DocumentExpireDate
            );
            
            distributor.FirstName = request.FirstName;
            distributor.LastName = request.LastName;
            distributor.BirthDate = request.BirthDate;
            distributor.Sex = request.Sex;
            distributor.Picture = request.Picture ?? distributor.Picture; // keep old pic if new isn't supplied
            distributor.Document = document;
            distributor.Contact = contactInfo;
            distributor.Address = address;
            
            _distributorRepository.Update(distributor);

            return new GeneralResponse(true) {Distributor = distributor};
        }

        public GeneralResponse Delete(int id)
        {
            var distributor = _distributorRepository.GetById(id);
            
            if (distributor == null)
                return new GeneralResponse(false) {Error = $"Distributor with id {id} does not exist."};

            _distributorRepository.Delete(distributor);
            
            return new GeneralResponse(true) {Distributor = distributor};
        }
    }
}