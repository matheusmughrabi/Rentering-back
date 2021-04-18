using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rentering.Accounts.Domain.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Repositories.QueryRepositories;
using Rentering.Accounts.Infra.Repositories.CUDRepositories;
using Rentering.Accounts.Infra.Repositories.QueryRepositories;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;
using Rentering.Contracts.Infra.Repositories.AuthRepositories;
using Rentering.Contracts.Infra.Repositories.CUDRepositories;
using Rentering.Contracts.Infra.Repositories.QueryRepositories;
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

            services.AddTransient<IAccountCUDRepository, AccountCUDRepository>();
            services.AddTransient<IAccountQueryRepository, AccountQueryRepository>();

            services.AddTransient<IRenterCUDRepository, RenterCUDRepository>();
            services.AddTransient<IRenterQueryRepository, RenterQueryRepository>();
            services.AddTransient<IRenterAuthRepository, RenterAuthRepository>();
            services.AddTransient<IAuthRenterService, AuthRenterService>();

            services.AddTransient<ITenantCUDRepository, TenantCUDRepository>();
            services.AddTransient<ITenantQueryRepository, TenantQueryRepository>();
            services.AddTransient<ITenantAuthRepository, TenantAuthRepository>();
            services.AddTransient<IAuthTenantService, AuthTenantService>();

            services.AddTransient<IContractAuthRepository, ContractAuthRepository>();

            services.AddTransient<IContractUserProfileCUDRepository, ContractUserProfileCUDRepository>();
            services.AddTransient<IContractUserProfileQueryRepository, ContractUserQueryRepository>();

            services.AddTransient<IContractCUDRepository, ContractCUDRepository>();
            services.AddTransient<IContractQueryRepository, ContractQueryRepository>();
            services.AddTransient<IAuthContractService, AuthContractService>();

            services.AddTransient<IContractPaymentCUDRepository, ContractPaymentCUDRepository>();
            services.AddTransient<IContractPaymentQueryRepository, ContractPaymentQueryRepository>();
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
