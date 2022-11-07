using AutoMapper;
using Microsoft.AspNetCore.Http;
using WebApi.Application.Abstract;
using WebApi.Application.Dto.Product.Request;
using WebApi.Application.Dto.Product.Response;
using WebApi.Application.Mapping;
using WebApi.Core.Common;
using WebApi.Core.Repositories;
using WebApi.Domain;
using WebApi.Domain.Interfaces;

namespace WebApi.Application.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
        }

        public async Task<Response<List<ProductResponse>>> Get()
        {
            var products = await _unitOfWork.Repository<IProductRepository>()
                .GetListAsync(a => a.IsActive && !a.IsDeleted, null, 0, 0, a => a.History);
            var productResults = _mapper.Map<List<ProductResponse>>(products);
            return Response<List<ProductResponse>>.CreateResponse(productResults, StatusCodes.Status200OK);

        }

        public async Task<Response<ProductResponse>> GetById(Guid id)
        {
            var product = await _unitOfWork.Repository<IProductRepository>().GetAsync(x => x.Id == id);
            var productResult = _mapper.Map<ProductResponse>(product);
            return Response<ProductResponse>.CreateResponse(productResult, StatusCodes.Status200OK);
        }

        public async Task AddProduct(CreateProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            _unitOfWork.Repository<IProductRepository>().Add(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateProduct(UpdateProductRequest request)
        {
            var repo = _unitOfWork.Repository<IProductRepository>();

            var product = await repo.GetAsync(a => a.Id == request.Id);
            product.LogHistory();

            _mapper.Map(request, product);

            repo.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProduct(Guid id)
        {
            var repo = _unitOfWork.Repository<IProductRepository>();
            var product = await repo.GetAsync(a => a.Id == id);
            repo.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
