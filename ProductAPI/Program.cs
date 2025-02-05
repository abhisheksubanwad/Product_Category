using ProductAPI.Mapping;
using ProductAPI.Repositories.IRepositories;
using ProductAPI.Repositories.SqlRepositories;

var builder = WebApplication.CreateBuilder(args);

// Register the EmployeeRepository
builder.Services.AddScoped<IProduceRepositorie, SqlProduceRepositorie>();
builder.Services.AddScoped<IImageService, SQLImageService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMappingProfile));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Allow requests from your React app
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
// Enable CORS globally
app.UseCors("AllowFrontend");
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();