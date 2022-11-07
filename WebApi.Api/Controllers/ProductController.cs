using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Abstract;
using WebApi.Application.Dto.Product.Request;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly ILogger _logger;

        public ProductController(IProductManager productManager, ILogger<ProductController> logger)
        {
            _productManager = productManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productManager.Get();


            _logger.LogInformation($"Total products: {result.Data.Count}");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _productManager.Get();

            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductRequest request)
        {
            await _productManager.UpdateProduct(request);

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateProductRequest request)
        {

            await _productManager.AddProduct(request);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productManager.DeleteProduct(id);
            return Ok();
        }
    }
}