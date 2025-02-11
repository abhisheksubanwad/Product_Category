using AutoMapper;
using MySqlConnector;
using ProductAPI.Model.Domain;
using ProductAPI.Model.Dto;
using ProductAPI.Repositories.IRepositories;
using System.Data;
using System.Text.Json;

namespace ProductAPI.Repositories.SqlRepositories
{
    public class SqlProductSubCategory : IProductSubCategory
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public SqlProductSubCategory(IConfiguration configuration, IMapper mapper, IImageService imageService)
        {
            _connectionString = configuration.GetConnectionString("MySQLDBString");
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ResponseDto> CreateProductSubCategoryAsync(ProductSubCategoryDto subCategoryDto)
        {
            try
            {
                string imageUrl = string.Empty;

                if (subCategoryDto.ImageUrl != null)
                {
                    imageUrl = await _imageService.SaveImageAsync(subCategoryDto.ImageUrl);
                }

                var jsonData = JsonSerializer.Serialize(new
                {
                    subCategoryDto.ProductCategoryId,
                    subCategoryDto.LanguageData,
                    ImageUrl = imageUrl
                });

                using (var connection = new MySqlConnection(_connectionString))
                using (var command = new MySqlCommand("CreateProductSubCategory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@jsonInput", jsonData);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    return new ResponseDto
                    {
                        Result = null,
                        IsSuccess = true,
                        Message = "Product Subcategory Created Successfully"
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


        public async Task<ResponseDto> GetAllProductSubCategoriesAsync()
        {
            try
            {
                var productSubCategories = new List<ProductSubCategory>();

                using (var connection = new MySqlConnection(_connectionString))
                using (var command = new MySqlCommand("GetAllProductSubCategories", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var category = new ProductCategory
                            {
                                ProductCategoryId = reader.GetGuid("Category_ProductCategoryId"),
                                Title = reader.GetString("Category_Title"),
                                Description = reader.GetString("Category_Description"),
                                ImageUrl = reader.GetString("Category_ImageUrl"),
                                Language = (Language)reader.GetInt32("Category_Language"),
                                CreatedDate = reader.GetDateTime("Category_CreatedDate"),
                                LastUpdatedDate = reader.GetDateTime("Category_LastUpdatedDate")
                            };

                            var subCategory = new ProductSubCategory
                            {
                                ProductSubCategoryId = reader.GetGuid("ProductSubCategoryId"),
                                ProductCategoryId = reader.GetGuid("ProductCategoryId"),
                                Title = reader.GetString("Title"),
                                Description = reader.GetString("Description"),
                                ImageUrl = reader.GetString("ImageUrl"),
                                Language = (Language)reader.GetInt32("Language"),
                                CreatedDate = reader.GetDateTime("CreatedDate"),
                                LastUpdatedDate = reader.GetDateTime("LastUpdatedDate"),
                                Category = category
                            };

                            productSubCategories.Add(subCategory);
                        }
                    }
                }

                return new ResponseDto { Result = productSubCategories, IsSuccess = true, Message = "Product subcategories retrieved successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }




    }
}
