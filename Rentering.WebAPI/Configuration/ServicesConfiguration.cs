using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Accounts.Infra.Data;
using Rentering.Accounts.Infra.Data.Repositories.CUDRepositories;
using Rentering.Accounts.Infra.Data.Repositories.QueryRepositories;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;
using Rentering.Contracts.Infra.Data;
using Rentering.Contracts.Infra.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Infra.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Infra.Services;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rentering.WebAPI.Configuration
{
    public static class ServicesConfiguration
    {
        public static void RegisterRepositoriesAndServices(this IServiceCollection services)
        {
            services.AddScoped<RenteringDataContext, RenteringDataContext>();

            #region Accounts
            services.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();

            services.AddTransient<IAccountCUDRepository, AccountCUDRepository>();
            services.AddTransient<IAccountQueryRepository, AccountQueryRepository>();
            #endregion

            #region Contracts
            services.AddScoped<IContractUnitOfWork, ContractUnitOfWork>();

            services.AddTransient<IAccountContractsCUDRepository, AccountContractsCUDRepository>();

            services.AddTransient<IRenterCUDRepository, RenterCUDRepository>();
            services.AddTransient<IRenterQueryRepository, RenterQueryRepository>();
            services.AddTransient<IAuthRenterService, AuthRenterService>();

            services.AddTransient<ITenantCUDRepository, TenantCUDRepository>();
            services.AddTransient<ITenantQueryRepository, TenantQueryRepository>();
            services.AddTransient<IAuthTenantService, AuthTenantService>();

            services.AddTransient<IGuarantorCUDRepository, GuarantorCUDRepository>();
            services.AddTransient<IGuarantorQueryRepository, GuarantorQueryRepository>();
            services.AddTransient<IAuthGuarantorService, AuthGuarantorService>();

            services.AddTransient<IEstateContractCUDRepository, EstateContractCUDRepository>();
            services.AddTransient<IEstateContractQueryRepository, EstateContractQueryRepository>();

            services.AddTransient<IContractPaymentCUDRepository, ContractPaymentCUDRepository>();
            services.AddTransient<IContractPaymentQueryRepository, ContractPaymentQueryRepository>();
            #endregion
        }

        public static void RegisterFluentMigrator(this IServiceCollection services)
        {
            services
                .AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddSqlServer2016()
                    .WithGlobalConnectionString(DatabaseSettings.connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All());

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
