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

// ============= RELEASE 1.2.2 =============
// EntityFramework
// AspNetIdentity
// Sistema de autorização de dados
// Criar todos os testes
// Corrigir eventuais bugs
// [TALVEZ] Mediatr

// ============= Implementação Entity Framework =============
// Estudar projeto exemplo -> NerdStore ON GOING
// Implementar projeto exemplo com alteração de estado em mais de uma classe -> testar se um SaveChanges já salva tudo ON GOING
// Aprofundamento em Entity Framework ON GOING

// Implementar EF em Accounts ON GOING
// Criar novo projeto Accounts.ApplicationEF e um AccountEFController com o objetivo de implementar sem quebrar nada e pegar o jeito do EF Core
// Implementar AccountUnitOfWork

// Implementar EF em Contracts
// Criar novo projeto Contracts.ApplicationEF e um ContractEFController
// Implementar ContractUnitOfWork

// Analisar estrutura e comparar com Dapper
// Criar uma estrutura limpa e que facilite a troca EF <---> Dapper


