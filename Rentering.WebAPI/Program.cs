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

// ====== NOVA BRANCH -> features/modifies-account-entity (nome está errado)======
// Criar tabela Renter OK
// Criar Stored Procedures Renter ON GOING
// Implementar Repositorios Renter ON GOING
// Testar
// Merge com release-1.2.1

// ====== NOVA BRANCH -> features/refactoring-repositories ======
// Organizar BD, StoredProcedures, Versionamento BD
// Testar
// Merge com release-1.2.1

// ====== NOVA BRANCH -> features/tenant-functionality ======
// Criar tabela Tenant
// Criar Stored Procedures Tenant
// Implementar Repositorios Tenant
// Testar
// Merge com release-1.2.1

// ====== NOVA BRANCH -> features/guarantor-functionality ======
// Criar tabela Guarantor
// Criar Stored Procedures Guarantor
// Implementar Repositorios Guarantor
// Testar
// Merge com release-1.2.1