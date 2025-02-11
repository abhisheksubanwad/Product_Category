using AutoMapper;
using MySqlConnector;
using ProductAPI.Model.Dto.Product;
using ProductAPI.Model.Dto;
using ProductAPI.Repositories.IRepositories;
using System.Data;
using System.Text.Json;
using ProductAPI.Model.Domain;


namespace ProductAPI.Repositories.SqlRepositories
{
    public class SqlProduct : IProduct
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public SqlProduct(IConfiguration configuration, IMapper mapper, IImageService imageService)
        {
            _connectionString = configuration.GetConnectionString("MySQLDBString");
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<ResponseDto> CreateProductAsync(ProductDto productDto, string productCategoryId, string productSubCategoryId)
        {
            try
            {
                var imageUrls = new List<string>();

              
                if (productDto.ImageUrls != null && productDto.ImageUrls.Count > 0)
                {
                    foreach (var image in productDto.ImageUrls) 
                    {
                        string imageUrl = await _imageService.SaveImageAsync(image);
                        imageUrls.Add(imageUrl);
                    }
                }

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand("CreateProduct", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Convert product data to JSON
                        string jsonInput = JsonSerializer.Serialize(new
                        {
                            ProductCategoryId = productCategoryId,
                            ProductSubCategoryId = productSubCategoryId,
                            productDto.Status,
                            productDto.Ingredients,
                            productDto.SeedRateDose,
                            productDto.PlantingSeason,
                            productDto.SowingPlantingSpacing,
                            productDto.FertilizerDoses,
                            productDto.NumberOfCuttings,
                            productDto.Yield,
                            productDto.AdvantagesBenefits,
                            productDto.PrecautionsStandards,
                            productDto.LanguageData,
                            ImageUrls = imageUrls,
                            productDto.ProductVideoUrls,
                            productDto.SKUs
                        });

                        command.Parameters.Add(new MySqlParameter("@jsonInput", MySqlDbType.Text) { Value = jsonInput });

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        return new ResponseDto
                        {
                            IsSuccess = rowsAffected > 0,
                            Message = rowsAffected > 0 ? "Product created successfully." : "Failed to create product.",
                            Result = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Result = null
                };
            }
        }

        public async Task<ResponseDto> GetAllProductsAsync()
        {
            try
            {
                var productCategories = new List<ProductCategory>();

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand("GetAllProducts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Read Product Category
                                Guid productCategoryId = reader.GetGuid("ProductCategoryId");
                                var productCategory = productCategories.FirstOrDefault(pc => pc.ProductCategoryId == productCategoryId);
                                if (productCategory == null)
                                {
                                    productCategory = new ProductCategory
                                    {
                                        ProductCategoryId = productCategoryId,
                                        Title = reader.GetString("ProductCategoryTitle"),
                                        Description = reader.GetString("ProductCategoryDescription"),
                                        ImageUrl = reader.GetString("ProductCategoryImage"),
                                        Language = Enum.Parse<Language>(reader.GetString("ProductCategoryLanguage")),
                                        CreatedDate = reader.GetDateTime("ProductCategoryCreatedDate"),
                                        LastUpdatedDate = reader.GetDateTime("ProductCategoryLastUpdatedDate")
                                    };
                                    productCategories.Add(productCategory);
                                }

                                // Read Product SubCategory
                                Guid productSubCategoryId = reader.GetGuid("ProductSubCategoryId");
                                var productSubCategory = productCategory.ProductSubCategories?.FirstOrDefault(psc => psc.ProductSubCategoryId == productSubCategoryId);
                                if (productSubCategory == null)
                                {
                                    productSubCategory = new ProductSubCategory
                                    {
                                        ProductSubCategoryId = productSubCategoryId,
                                        ProductCategoryId = reader.GetGuid("SubCategory_ProductCategoryId"),
                                        Title = reader.GetString("ProductSubCategoryTitle"),
                                        Description = reader.GetString("ProductSubCategoryDescription"),
                                        ImageUrl = reader.GetString("ProductSubCategoryImage"),
                                        Language = Enum.Parse<Language>(reader.GetString("ProductSubCategoryLanguage")),
                                        CreatedDate = reader.GetDateTime("ProductSubCategoryCreatedDate"),
                                        LastUpdatedDate = reader.GetDateTime("ProductSubCategoryLastUpdatedDate")
                                    };

                                    if (productCategory.ProductSubCategories == null)
                                        productCategory.ProductSubCategories = new List<ProductSubCategory>();

                                    productCategory.ProductSubCategories.Add(productSubCategory);
                                }

                                // Read Product
                                var product = new Product
                                {
                                    ProductId = reader.GetGuid("ProductId"),
                                    ProductCategoryId = reader.GetGuid("Product_ProductCategoryId"),
                                    ProductSubCategoryId = reader.GetGuid("Product_ProductSubCategoryId"),
                                    Name = reader.GetString("ProductName"),
                                    Description = reader.GetString("ProductDescription"),
                                    Status = reader.GetBoolean("Status"),
                                    ImageUrls = JsonSerializer.Deserialize<List<string>>(reader.GetString("ImageUrls")),
                                    ProductVideoUrls = JsonSerializer.Deserialize<List<string>>(reader.GetString("ProductVideoUrls")),
                                    Ingredients = reader.GetString("Ingredients"),
                                    SeedRateDose = reader.GetString("SeedRateDose"),
                                    PlantingSeason = reader.GetString("PlantingSeason"),
                                    SowingPlantingSpacing = reader.GetString("SowingPlantingSpacing"),
                                    FertilizerDoses = reader.GetString("FertilizerDoses"),
                                    NumberOfCuttings = reader.GetString("NumberOfCuttings"),
                                    Yield = reader.GetString("Yield"),
                                    AdvantagesBenefits = reader.GetString("AdvantagesBenefits"),
                                    PrecautionsStandards = reader.GetString("PrecautionsStandards"),
                                    CreatedDate = reader.GetDateTime("ProductCreatedDate"),
                                    LastUpdatedDate = reader.GetDateTime("ProductLastUpdatedDate"),
                                    Language = Enum.Parse<Language>(reader.GetString("ProductLanguage"))
                                };

                                if (productSubCategory.Products == null)
                                    productSubCategory.Products = new List<Product>();

                                productSubCategory.Products.Add(product);
                            }
                        }
                    }
                }

                return new ResponseDto
                {
                    IsSuccess = true,
                    Message = "Products retrieved successfully.",
                    Result = productCategories
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                    Result = null
                };
            }
        }





    }
}
