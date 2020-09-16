using System;
using System.Collections.Generic;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Input.UpdateRequests;
using MultilevelMarketing.Application.Interfaces;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Interfaces;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Application.Services
{
    public class SaleService : ISaleService
    {
        private ISaleRepository _saleRepository;
        private IDistributorRepository _distributorRepository;
        private IProductRepository _productRepository;
    
        public SaleService(ISaleRepository saleRepository,
                           IDistributorRepository distributorRepository,
                           IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _distributorRepository = distributorRepository;
            _productRepository = productRepository;
        }

        public GeneralResponse Add(AddSaleRequest addSaleRequest)
        {
            var sale = ProcessAddRequest(addSaleRequest, out GeneralResponse response);
            
            if (response.Success) _saleRepository.Add(sale);
            return response;
        }

        public GeneralResponse Update(UpdateSaleRequest updateSaleRequest)
        {
            var sale = ProcessUpdateRequest(updateSaleRequest, out GeneralResponse response);
            
            if (response.Success) _saleRepository.Update(sale);
            return response;
        }
        
        public GeneralResponse Delete(int id)
        {
            var sale = _saleRepository.GetById(id);
            
            if (sale == null) return new GeneralResponse(false) {Error = $"Sale with id {id} does not exist."};

            _saleRepository.Delete(sale);
            
            return new GeneralResponse(true) {Sale = sale};
        }

        public IEnumerable<Sale> Filter(FilterSalesRequest filterSalesRequest)
        {
            return _saleRepository.Filter(
                filterSalesRequest.DistributorId, filterSalesRequest.ProductId,
                filterSalesRequest.StartDate, filterSalesRequest.EndDate
            );
        }
        
        public IEnumerable<Sale> GetAll()
        { 
            return _saleRepository.GetAll();
        }

        public GeneralResponse GetById(int id)
        {
            var sale = _saleRepository.GetById(id);
            
            return new GeneralResponse(sale != null)
            {
                Sale = sale,
                Error = sale != null ? null : $"Sale with id {id} does not exist."
            };
        }
        
        /// <summary>
        /// checks if given distributor and sale exist. if they are, returns converted model
        /// </summary>
        /// <param name="addSaleRequest">AddSaleRequest to be converted</param>
        /// <param name="response">if checks fail, returns response with error message</param>
        /// <returns>converted AddSaleRequest</returns>
        private Sale? ProcessAddRequest(AddSaleRequest addSaleRequest, out GeneralResponse response)
        {
            var distributor = _distributorRepository.GetById((int) addSaleRequest.Distributor);

            if (distributor == null)
            {
                response = new GeneralResponse(false) {Error = $"Distributor with id {addSaleRequest.Distributor} does not exist."};
                return null;
            }

            var product = _productRepository.GetById((int) addSaleRequest.Product);

            if (product == null)
            {
                response = new GeneralResponse(false) {Error = $"Product with id {addSaleRequest.Product} does not exist."};
                return null;
            }

            var sale = new Sale(
                distributor.Id,
                product.Id,
                (DateTime) addSaleRequest.Date,
                (decimal) addSaleRequest.Price,
                (decimal) addSaleRequest.SeparatePrice,
                (decimal) addSaleRequest.TotalPrice
            );
            
            response = new GeneralResponse(true) {Sale = sale};
            return sale;
        }

        private Sale? ProcessUpdateRequest(UpdateSaleRequest updateSaleRequest, out GeneralResponse response)
        {
            var id = (int) updateSaleRequest.Id;
            
            if (_saleRepository.GetById(id) == null)
            {
                response = new GeneralResponse(false)
                    {Error = $"Distributor with id {updateSaleRequest.Distributor} does not exist."};
                return null;
            }
            
            var sale = ProcessAddRequest(updateSaleRequest, out GeneralResponse innerResponse);
            response = innerResponse;
            if (sale != null)
                sale.Id = id; // ProcessAddRequest maps everything but id
            
            return sale;
        }
    }
}