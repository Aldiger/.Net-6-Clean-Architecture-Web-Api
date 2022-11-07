namespace WebApi.Application.Dto.Product.Request
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

    }
}
