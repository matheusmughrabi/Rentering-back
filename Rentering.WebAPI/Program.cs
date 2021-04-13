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
// Alterar accounts para ter apenas email, username and password
// Alterar BD accounts
// Testar

// Criar tabelas Renter, Tenant e Guarantor
// Criar stored procedures no padrão sp_NomeTabela_CUD/Query/Auth_NomeStoredProcedure
// Testar

// Alterar nomes dos Stored Procedures para nomenclatura nova
// Testar