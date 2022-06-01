using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductManagement.Application.Interface;
using ProductManagement.Application.Service;
using ProductManagement.Contexts;
using ProductManagement.Infrastructure.Extensions;
using System.Globalization;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//
builder.Services.AddMvc(o =>
{
    o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "The value '{0}' is not valid for {1}");
    o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "A value for the '{0}' property was not provided");
    o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "A value is required.");
    o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "A non-empty request body is required.");
    o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "The value '{0}' is not valid.");
    o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "The supplied value is invalid.");
    o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "The field must be a number.");
    o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "The supplied value is invalid for {0}.");
    o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "The value '{0}' is invalid.");
    o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "The field {0} must be a number.");
    o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "The field {0} not must null");

    //  options.ModelBindingMessageProvider.s

}).AddJsonOptions(options =>
{
    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
}).AddDataAnnotationsLocalization();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


#region ConnectionString

string connection = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection), ServiceLifetime.Scoped);
builder.Services.AddSqlServer<DataBaseContext>(connection);

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Description = "Docs for my API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});




builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{

    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Encryptionkey"]))
    };
});

Registers(builder.Services);

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          builder =>
                          {
                              builder
                              //.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                 .SetIsOriginAllowed((host) => true)
                                 .AllowCredentials();
                          });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region localizer

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                 new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

#endregion

/// <summary>
/// /////////////// app //////////////
/// </summary>

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

#region language Resource

var supportedCultures = new List<CultureInfo>
                                {
                                    new CultureInfo("en-US"),
                                };

var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
};

app.UseRequestLocalization(options);

#endregion

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Create By Moj Team");
    c.RoutePrefix = string.Empty;
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});


app.UseStaticFiles();

app.MapControllers();

app.Run();


void Registers(IServiceCollection services)
{
    services.AddScoped<IDataBaseContext, DataBaseContext>();
    services.RegisterServices();
}