using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Rentering.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

// Create Guarantor command, handler, CUDrepository and tests OK
// Alterar accounts para ter apenas email, username and password OK
// Alterar BD accounts OK
// Testar OK

// Criar tabela Renter OK
// Criar Stored Procedures Renter
// Implementar Repositorios Renter
// Testar

// Criar tabela Tenant
// Criar Stored Procedures Tenant
// Implementar Repositorios Tenant
// Testar

// Criar tabela Guarantor
// Criar Stored Procedures Guarantor
// Implementar Repositorios Guarantor
// Testar

// Alterar nomes dos Stored Procedures para nomenclatura nova
// Testar

// Estudar versionamento do BD -> Talvez utilizar migration do Entity Framework e Dapper para acesso