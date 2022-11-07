using WebApi.Application.Dto.Product.Request;
using WebApi.Application.Dto.Product.Response;
using WebApi.Core.Common;

namespace WebApi.Application.Abstract
{
    public interface IProductManager
    {
        Task<Response<List<ProductResponse>>> Get();
        Task<Response<ProductResponse>> GetById(Guid id);
        Task AddProduct(CreateProductRequest request);
        Task UpdateProduct(UpdateProductRequest request);
        Task DeleteProduct(Guid id);
    }
}
