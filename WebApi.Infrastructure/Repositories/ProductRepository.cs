using WebApi.Core.Repositories;
using WebApi.Domain;
using WebApi.Domain.Interfaces;
using WebApi.Infrastructure.Context;

namespace WebApi.Infrastructure.Repositories;

public class ProductRepository : Repository<Product, WebApiContext>, IProductRepository
{
    public ProductRepository(WebApiContext context) : base(context)
    {
    }
}