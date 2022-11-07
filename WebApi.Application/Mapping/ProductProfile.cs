using AutoMapper;
using WebApi.Application.Dto.Product.Request;
using WebApi.Application.Dto.Product.Response;
using WebApi.Domain;

namespace WebApi.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();

            CreateMap<Product, ProductResponse>();


        }
    }
}
