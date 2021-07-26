using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rentering.Accounts.Domain.Data;
using Rentering.Contracts.Domain.Data;
using Rentering.Corporation.Domain.Data;
using Rentering.Infra;
using Rentering.Infra.Accounts;
using Rentering.Infra.Contracts;
using Rentering.Infra.Corporations;
using System.Collections.Generic;
using System.Text;

namespace Rentering.WebAPI.Configuration
{
    public static class ServicesConfiguration
    {
        public static void RegisterEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RenteringDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();
            services.AddScoped<IContractUnitOfWork, ContractUnitOfWork>();
            services.AddScoped<ICorporationUnitOfWork, CorporationUnitOfWork>();
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rentering API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
               "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
               "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
               "Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void RegisterAuthenticationAndAuthorization(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Settings.secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
