using ProductAPI.Model.Dto;
using AutoMapper;
using MySqlConnector;
using ProductAPI.Repositories.IRepositories;
using System.Data;
using System.Text.Json;


public class SqlProduceRepositorie : IProduceRepositorie
{
    private readonly string _connectionString;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public SqlProduceRepositorie(IConfiguration configuration, IMapper mapper, IImageService imageService)
    {
        _connectionString = configuration.GetConnectionString("MySQLDBString");
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<ResponseDto> AddProductHierarchyAsync(LanguageDto productDto, string imageUrl)
    {
        var languageData = productDto.LanguageData.Select(lang => new
        {
            language = lang.language.ToString(),
            title = lang.Title,
            description = lang.Description
        }).ToList();

        // Prepare JSON for stored procedure
        var jsonData = JsonSerializer.Serialize(new
        {
            ImageUrl = imageUrl,
            Status = productDto.Status,
            LanguageData = languageData,
            ProductCategory = new { }, // Optional
            ProductSubCategory = new { }, // Optional
            Product = new { } // Optional
        });

        using (var connection = new MySqlConnection(_connectionString))
        {
            using (var command = new MySqlCommand("CreateProductHierarchy", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@jsonInput", jsonData);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Message = "Product hierarchy created successfully."
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = $"Error: {ex.Message}"
                    };
                }
            }
        }
    }

}
