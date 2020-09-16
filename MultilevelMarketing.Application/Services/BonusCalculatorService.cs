using System;
using System.Collections.Generic;
using System.Linq;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Interfaces;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Interfaces;
using MultilevelMarketing.Domain.Configs;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Application.Services
{
    public class BonusCalculatorService : IBonusCalculatorService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDistributorRepository _distributorRepository;

        public BonusCalculatorService(ISaleRepository saleRepository, IDistributorRepository distributorRepository)
        {
            _saleRepository = saleRepository;
            _distributorRepository = distributorRepository;
        }
        
        public BonusCalculatorResponse CalculateBonus(BonusCalculatorRequest request)
        {
            var distributor = _distributorRepository.GetById((int) request.Distributor);
            if (distributor == null)
                return new BonusCalculatorResponse(false)
                {
                    Error = $"Distributor with id {request.Distributor} does not exist"
                };

            var selfMadeSales = _saleRepository.GetSalesBetween(distributor.Id,
                request.StartDate, request.EndDate)
                .Where(s => s.BonusCollected == false)
                .ToList();
            
            // get list of referral hierarchy
            var referredDistributors =
                _distributorRepository.GetDescendantReferrers(distributor,
                    BonusCalculatorConfig.ReferredSaleBonuses.Length);

            // grab sales of each referral layer
            var referralSales = Enumerable.Range(0, referredDistributors.Length)
                .Select(i => GetReferralSales(referredDistributors[i], request.StartDate,
                    request.EndDate))
                .ToList();

            // sum each referral layer
            var referralLayerSums = referralSales
                .Select(i => i.Sum(sale => sale.TotalPrice))
                .ToList();

            // multiply each sale by coefficient from config
            var calculatedLayerSums = Enumerable.Range(0, referralLayerSums.Count)
                .Select(i => referralLayerSums[i] * BonusCalculatorConfig.ReferredSaleBonuses[i])
                .ToList();

            var totalBonus = selfMadeSales.Sum(i => i.TotalPrice) * BonusCalculatorConfig.SelfMadeSalesBonus +
                             calculatedLayerSums.Sum();

            // set BonusCollected to true and update database to 
            var allSales = selfMadeSales.Concat(referralSales.SelectMany(i => i)).ToList();
            foreach (var sale in allSales)
                sale.BonusCollected = true;
            _saleRepository.UpdateRange(allSales);
            
            return new BonusCalculatorResponse(true)
            {
                TotalBonus = totalBonus,
                ReferralBonuses = calculatedLayerSums
            };
        }

        private List<Sale> GetReferralSales(IEnumerable<Distributor> distributors, DateTime startDate, DateTime endDate)
        {
            var sales = new List<Sale>();
            foreach (var distributor in distributors)
            {
                sales.AddRange(
                _saleRepository.GetSalesBetween(distributor.Id, startDate, endDate)
                    .Where(s => s.BonusCollected == false)
                    );
            }

            return sales;
        }
    }
}