using Aqarlist.Core.Models.Database;
using Aqarlist.Core.Models.Dto;
using Aqarlist.Core.Services.Service_Implemetation;
using Aqarlist.Core.Services.Service_Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Scrutor;
using System.Reflection;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Amazon;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.Scan(scan => scan
    .FromAssemblies(Assembly.GetExecutingAssembly()) // Scan the current assembly
    .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Service"))) // Find classes ending with "Service"
    .AsImplementedInterfaces() // Register them as their interfaces
    .WithScopedLifetime() // Use Scoped lifetime (change as needed)
);
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddMvcCore();
var jwtSettings = configuration.GetSection("Jwt");
var googleSignIn = configuration.GetSection("GoogleSignIn");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
//builder.Configuration
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    .AddEnvironmentVariables();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
//builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddDefaultAWSOptions(new AWSOptions
{
    Region = RegionEndpoint.USEast1, // Updated to us-east-1
    Credentials = new BasicAWSCredentials(
        configuration["AWS:AccessKey"], // Ensure these keys are securely managed
        configuration["AWS:SecretKey"]
    )
});
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true, // need to see 
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    })
     .AddGoogle(options =>
     {
         options.ClientId = googleSignIn["ClientId"];
         options.ClientSecret = googleSignIn["ClientSecret"];
     });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Users, UserDto>();
    cfg.CreateMap<UserDto, Users>();
    cfg.CreateMap<PropertyType, PropertyTypeDto>();
    cfg.CreateMap<PropertyTypeDto, PropertyType>();
    cfg.CreateMap<PropertyDto, Property>();
    cfg.CreateMap<Property, PropertyDto>();
});

var app = builder.Build();

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
