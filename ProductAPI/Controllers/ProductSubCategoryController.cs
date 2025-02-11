using Microsoft.AspNetCore.Mvc;
using ProductAPI.Repositories.IRepositories;
using ProductAPI.Model.Dto;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSubCategoryController : ControllerBase
    {
        private readonly IProductSubCategory _productSubCategory;
        private readonly IImageService _imageService;

        public ProductSubCategoryController(IProductSubCategory productSubCategory, IImageService imageService)
        {
            _productSubCategory = productSubCategory;
            _imageService = imageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProductSubCategory([FromForm] ProductSubCategoryDto subCategoryDto)
        {
            var response = await _productSubCategory.CreateProductSubCategoryAsync(subCategoryDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProductSubCategories()
        {
            var response = await _productSubCategory.GetAllProductSubCategoriesAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }



    }
}