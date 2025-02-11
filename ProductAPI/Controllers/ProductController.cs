using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model.Dto;
using ProductAPI.Model.Dto.Product;
using ProductAPI.Repositories.IRepositories;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        private readonly IImageService _imageService;

        public ProductController(IProduct product, IImageService imageService)
        {
            _product = product;
            _imageService = imageService;
        }



        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromForm] string productCategoryId, [FromForm] string productSubCategoryId, [FromForm] ProductDto productDto)
        {
            try
            {
                var response = await _product.CreateProductAsync(productDto, productCategoryId, productSubCategoryId);

                if (response.IsSuccess)
                    return Ok(response);

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto
                {
                    IsSuccess = false,
                    Message = $"Server Error: {ex.Message}",
                    Result = null
                });
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _product.GetAllProductsAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }






    }
}
