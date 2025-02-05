using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model.Dto;
using ProductAPI.Repositories.IRepositories;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduceRepositorie _produceRepositorie;
        private readonly IImageService _imageService;

        public ProductController(IProduceRepositorie produceRepositorie, IImageService imageService)
        {
            _produceRepositorie = produceRepositorie;
            _imageService = imageService;
        }


        [HttpPost("CreateProductHierarchy")]
        public async Task<IActionResult> CreateProductHierarchy([FromForm] LanguageDto productDto)
        {
            if (productDto.ImageUrl == null || productDto.ImageUrl.Length == 0)
            {
                return BadRequest("Image file is required.");
            }

            if (productDto.LanguageData == null || !productDto.LanguageData.Any())
            {
                return BadRequest("LanguageData is required.");
            }

            string imageUrl;
            try
            {
                imageUrl = await _imageService.SaveImageAsync(productDto.ImageUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving image: {ex.Message}");
            }

            var response = await _produceRepositorie.AddProductHierarchyAsync(productDto, imageUrl);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return StatusCode(500, response.Message);
        }


    }
}
