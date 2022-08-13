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
// TODO: Services
builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.Configure<TokenModel>(builder.Configuration.GetSection("JWT"));
var token = builder.Configuration.GetSection("JWT").Get<TokenModel>();
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
        ValidAudience = builder.Configuration["JWT" + ':' + "ValidateAudience"],
        ValidIssuer = builder.Configuration["JWT" + ':' + "ValidateIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                .GetBytes(builder.Configuration["JWT" + ':' + "TokenSecret"]))
    };
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder => builder
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

app.UseCors("MyCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();

app.UseAuthentication();

app.UseAuthorization();