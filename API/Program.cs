using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using VETAPPAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "VETAPI",
        Description = "An ASP.NET Core Web API for managing VETAPI items",
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}); builder.Services.AddControllers().AddJsonOptions(options =>


{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddDbContext<VETDBContext>(options =>
    { 
        options.UseSqlServer(builder.Configuration.GetConnectionString("VetDB"), sqlServerOptions => {
            sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Maximum number of retries
                        maxRetryDelay: TimeSpan.FromSeconds(30), // Delay between retries
                        errorNumbersToAdd: null);
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// UseUrls to specify the URL where the API should listen
/// 192.168.0.14
//var url = "http://localhost:192.168.0.14:5000"; // Replace with your desired URL and port
app.Run();
