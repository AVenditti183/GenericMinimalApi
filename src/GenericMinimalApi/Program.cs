
using GenericMinimalApi.Data;
using GenericMinimalApi.Infrastructures;
using GenericMinimalApi.Repository;
using GenericMinimalApi.Services;
using GenericMinimalApi.Http;
using GenericMinimalApi.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.TagActionsBy(api =>
        {
            if(api.ActionDescriptor is ControllerActionDescriptor description)
                return new string[] { description.ControllerName };

            return new string[] { api.GroupName  };

        }
    );
    c.DocInclusionPredicate((name, api) => true);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddScoped<IRepository<Book,int>,BookRepository>();
builder.Services.AddScoped(typeof(ICRUDService<,,,,,>),typeof(CRUDService<,,,,,>));
builder.Services.AddAutoMapper(o => o.AddProfile(new MappingProfiles()));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//app.MapGet("/", () => "Hello World!");

//app.MapBook();

app.Run();

