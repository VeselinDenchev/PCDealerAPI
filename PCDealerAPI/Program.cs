using System.Text;

using Data.DbContext;
using Data.Models.Entities;
using Data.Services.DtoModels.Jwt;
using Data.Services.JWT;
using Data.Services.MapProfiles;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Data.Services.JWT.Interfaces;
using Data.Services.EntityServices.Interfaces;
using Data.Services.EntityServices;
using Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TotalErrorWebAPI",
        Version = "v1",
    });
});

builder.Services.AddScoped<PcDealerDbContext>();
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.Configure<TokenModel>(builder.Configuration.GetSection(JsonConstant.JWT_OBJECT));
var token = builder.Configuration.GetSection(JsonConstant.JWT_OBJECT).Get<TokenModel>();
var secret = Encoding.ASCII.GetBytes(token.TokenSecret);

builder.Services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<PcDealerDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

// JWT start
builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(b =>
{
    b.SaveToken = true;
    b.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration[$"{JsonConstant.JWT_OBJECT}:{JsonConstant.VALIDATE_AUDIENCE_PROPERTY}"],
        ValidIssuer = builder.Configuration[$"{JsonConstant.JWT_OBJECT}:{JsonConstant.VALIDATE_ISSUER_PROPERTY}"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                .GetBytes(builder.Configuration[$"{JsonConstant.JWT_OBJECT}:{JsonConstant.TOKEN_SECRET_PROPERTY}"]))
    };
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(ControllerConstant.CORS_POLICY, builder => builder
        //.WithOrigins("http://localhost:3000/")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header"));
});

builder.Services.AddAuthorization();
// JWT end

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(ControllerConstant.CORS_POLICY);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();

app.UseAuthentication();

app.UseAuthorization();