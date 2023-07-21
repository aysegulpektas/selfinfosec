using API.ConfigurationModels;
using Business.BusinessServiceRegistration;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Results;
using DataAccess.DataAccessServiceRegistration;
using Entities.Concrete.DTOs;
using Entities.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.DataAccessServiceRegister();
builder.Services.AddHttpContextAccessor();
builder.Services.BusinessServiceRegister();
var currentEnv = builder.Environment;
builder.Services.AddSingleton<IWebHostEnvironment>(currentEnv);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


/* JWT Security */

var jwtSecurityConfig = builder.Configuration.GetSection("JWTSecurity").Get<JWTConfig>();
var secretKey = Encoding.ASCII.GetBytes(jwtSecurityConfig.SigningKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSecurityConfig.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});


builder.Services.AddControllers().AddFluentValidation().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = c =>
    {
        var errorlist = c.ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors);
        List<string> errors = new List<string>();
        foreach (var error in errorlist)
        {
            errors.Add(error.ErrorMessage);
        }
        //string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
        //.SelectMany(v => v.Errors)
        //.Select(v => v.ErrorMessage));

        return new BadRequestObjectResult(new ErrorDataResult<List<String>>("Dogrulama Hatasi", errors));

    };
});

builder.Services.AddTransient<IValidator<User>, UserValidation>();

builder.Services.AddTransient<IValidator<UserRegisterModel>, UserRegisterModelValidation>();

builder.Services.AddTransient<IValidator<Subcategory>, SubcategoryValidation>();

builder.Services.AddTransient<IValidator<Category>, CategoryValidation>();


builder.Services.AddCors();

var app = builder.Build();
/*------------ */
// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
    .AllowCredentials()); // allow credentials
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    await next();

    if (!Path.HasExtension(context.Request.Path.Value) &&
        !context.Request.Path.Value.StartsWith("/api/"))
    {
        context.Request.Path = "/index.html";
        await next();
    }
});

app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
app.UseStaticFiles();
app.MapControllers();
app.Run();





