
using GenericMinimalApi.Data;
using GenericMinimalApi.Http;
using GenericMinimalApi.Infrastructures;
using GenericMinimalApi.Mapping;
using GenericMinimalApi.Repository;
using GenericMinimalApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

#region MinimalAPi

app.MapBook();

#endregion

app.Run();

