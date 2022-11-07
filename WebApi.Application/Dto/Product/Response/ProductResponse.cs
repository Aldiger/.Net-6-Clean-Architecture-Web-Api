using WebApi.Domain;

namespace WebApi.Application.Dto.Product.Response
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<ProductHistory> History { get; set; }

    }
}
