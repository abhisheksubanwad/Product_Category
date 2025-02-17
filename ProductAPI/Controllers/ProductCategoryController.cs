using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model.Dto.Product_Category;
using ProductAPI.Repositories.IRepositories;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategory _productCategory;
        private readonly IImageService _imageService;

        public ProductCategoryController(IProductCategory productCategory, IImageService imageService)
        {
            _productCategory = productCategory;
            _imageService = imageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductCategory([FromForm] ProductCategoryDto categoryDto)
        {
            var response = await _productCategory.CreateProductCategoryAsync(categoryDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpGet("Get-all")]
        public async Task<IActionResult> GetAllProductCategories()
        {
            var response = await _productCategory.GetAllProductCategoriesAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

    }
}
