using System.Collections.Generic;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Input.UpdateRequests;
using MultilevelMarketing.Application.Interfaces;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Interfaces;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
    
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public GeneralResponse Add(AddProductRequest addProductRequest)
        {
            var product = new Product(
                addProductRequest.Code,
                addProductRequest.Name,
                (decimal) addProductRequest.Price
            );

            _productRepository.Add(product);
            return new GeneralResponse(true) {Product = product}; 
        }

        public GeneralResponse Update(UpdateProductRequest updateProductRequest)
        {
            var product = _productRepository.GetById((int) updateProductRequest.Id);
            if (product == null)
                return new GeneralResponse(false) {Error = $"Product with id {updateProductRequest.Id} does not exist."};

            product.Code = updateProductRequest.Code;
            product.Name = updateProductRequest.Name;
            product.Price = (decimal) updateProductRequest.Price;
            
            _productRepository.Update(product);
            return new GeneralResponse(true) {Product = product};
        }

        public GeneralResponse Delete(int id)
        {
            var product = _productRepository.GetById(id);
            
            if (product == null)
                return new GeneralResponse(false) {Error = $"Product with id {id} does not exist."};

            _productRepository.Delete(product);
            
            return new GeneralResponse(true) {Product = product};
        }

        public IEnumerable<Product> GetAll()
        { 
            return _productRepository.GetAll();
        }

        public GeneralResponse GetById(int id)
        {
            var product = _productRepository.GetById(id);
            
            return new GeneralResponse(product != null)
            {
                Product = product,
                Error = product != null ? null : $"Product with id {id} does not exist."
            };
        }
    }
}