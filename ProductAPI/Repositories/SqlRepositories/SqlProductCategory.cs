using AutoMapper;
using MySqlConnector;
using ProductAPI.Model.Dto.Product_Category;
using ProductAPI.Model.Dto;
using ProductAPI.Repositories.IRepositories;
using System.Data;
using System.Text.Json;
using ProductAPI.Model.Domain;

namespace ProductAPI.Repositories.SqlRepositories
{
    public class SqlProductCategory : IProductCategory
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public SqlProductCategory(IConfiguration configuration, IMapper mapper, IImageService imageService)
        {
            _connectionString = configuration.GetConnectionString("MySQLDBString");
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ResponseDto> CreateProductCategoryAsync(ProductCategoryDto categoryDto)
        {
            try
            {
                string imageUrl = string.Empty;

                if (categoryDto.ImageUrl != null)
                {
                    imageUrl = await _imageService.SaveImageAsync(categoryDto.ImageUrl);
                }

                var jsonData = JsonSerializer.Serialize(new
                {
                    categoryDto.LanguageData,
                    ImageUrl = imageUrl
                });

                using (var connection = new MySqlConnection(_connectionString))
                using (var command = new MySqlCommand("CreateProductCategory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@jsonInput", jsonData);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    return new ResponseDto
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Product Category Created Successfully"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<ResponseDto> GetAllProductCategoriesAsync()
        {
            try
            {
                var productCategories = new List<ProductCategory>();

                using (var connection = new MySqlConnection(_connectionString))
                using (var command = new MySqlCommand("GetAllProductCategories", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productCategories.Add(new ProductCategory
                            {
                                ProductCategoryId = reader.GetGuid("ProductCategoryId"),
                                Title = reader.GetString("Title"),
                                Description = reader.GetString("Description"),
                                ImageUrl = reader.GetString("ImageUrl"),
                                Language = (Language)reader.GetInt32("Language"),
                                CreatedDate = reader.GetDateTime("CreatedDate"),
                                LastUpdatedDate = reader.GetDateTime("LastUpdatedDate")
                            });
                        }
                    }
                }

                return new ResponseDto { Result = productCategories, IsSuccess = true, Message = "Product categories retrieved successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }






    }
}
