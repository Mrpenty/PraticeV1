using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PracticeV1.Application.DTO.Category;
using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.Repositories;
using PracticeV1.Application.Repository;
using PracticeV1.Application.Service;
using PracticeV1.Application.Services;
using PracticeV1.Application.Validation;
using PracticeV1.Business.Repository;
using PracticeV1.Business.Service.Product;
using PracticeV1.Domain.Entity;
using PracticeV1.Infrastructure.Data;
using PracticeV1.Infrastructure.Repository;
using PracticeV1.Infrastructure.Service;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
   
builder.Services.AddDbContext<PRDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Mycnn")));
//builder.Services.AddDbContext<PRDBContext>(options =>
//    options.UseNpgsql(
//        builder.Configuration.GetConnectionString("Mycnn")));

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;              
    options.Password.RequireLowercase = false;          
    options.Password.RequireUppercase = false;          
    options.Password.RequireNonAlphanumeric = false;    
    options.Password.RequiredLength = 4;                
    options.Password.RequiredUniqueChars = 1;

    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<PRDBContext>()
.AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        NameClaimType = JwtRegisteredClaimNames.Sub,
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PracticeV1 API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>();
builder.Services.AddScoped<IOderService, OderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IValidator<CreateOrder>, CreateOrderValidator>();
builder.Services.AddScoped<IValidator<OrderItemCreate>, OrderItemCreateValidator>();
builder.Services.AddScoped<IValidator<CategoryCreate>, CategoryValidation>();


///Validation 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});



var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();



app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
