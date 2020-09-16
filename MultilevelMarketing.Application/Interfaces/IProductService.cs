using System.Collections.Generic;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Input.UpdateRequests;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Application.Interfaces
{
    public interface IProductService
    {
        public GeneralResponse Add(AddProductRequest addProductRequest);

        public GeneralResponse Update(UpdateProductRequest updateProductRequest);

        public GeneralResponse Delete(int id);

        public GeneralResponse GetById(int id);
        
        public IEnumerable<Product> GetAll();
    }
}