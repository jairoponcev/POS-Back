using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "POS Api",
                Version = "v1",
                Description = "Punto de venta API 2023",
                TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
                Contact = new OpenApiContact
                {
                    Name = "SIR TECH S.A.C",
                    Email = "correopruebas920@gmail.com",
                    Url = new Uri("https://sirtech.com.hn")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            };

            services.AddSwaggerGen(x =>
            {
                openApi.Version = "v1";
                x.SwaggerDoc("v1", openApi);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[]{} }
                });
            });

            return services;
        }
    }
}
