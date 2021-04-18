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

// ====== NOVA BRANCH -> features/accounts ======
// Create Guarantor command, handler, CUDrepository and tests OK
// Alterar accounts para ter apenas email, username and password OK
// Alterar BD accounts OK
// Testar OK


// ====== NOVA BRANCH -> features/renter-crud ======
// Criar tabela Renter OK
// Criar Stored Procedures Renter OK
// Implementar Repositorios Renter OK
// Criar unit tests OK
// Testar OK 
// Merge com release-1.2.1 OK


// ====== NOVA BRANCH -> features/renter-add-validations ======
// Adicionar valida��es nos ValueObjects OK
// Spouse atributos nao precisa se n�o for casado OK
// Testar OK
// Merge com release-1.2.1 OK


// ====== NOVA BRANCH -> features/refactoring-repositories ======
// Adicionar Fluent Migrator OK
// Organizar BD, StoredProcedures, Versionamento BD OK
// Testar OK
// Merge com release-1.2.1 OK


// ====== NOVA BRANCH -> features/refactoring-namings ======
// Corrigir commandHandlers nomes para Handlers OK
// Testar OK
// Merge com release-1.2.1 OK


// ====== NOVA BRANCH -> features/check-if-account ======
// Implementar checkIfAccounts OK
// Criar UnitTests para handlers OK
// Testar OK
// Merge com release-1.2.1 OK


// ====== NOVA BRANCH -> features/general-improvements ======
// Nomenclaturas OK
// Migrationg OK
// Testar OK
// Merge com release-1.2.1 OK


// ====== NOVA BRANCH -> features/tenant-functionality ======
// Criar handlers OK
// Criar unit tests OK
// Criar tabela Tenant OK
// Criar Stored Procedures Tenant OK
// Implementar Repositorios Tenant OK
// Testar ON GOING -> Corrigir bugs em renterService e tenantService, al�m disso � preciso criar testes para estes services OK
// Criar query repositories e controllers
// Testar
// Merge com release-1.2.1


// ====== NOVA BRANCH -> features/guarantor-functionality ======
// Criar handlers
// Criar unit tests
// Criar tabela Guarantor
// Criar Stored Procedures Guarantor
// Implementar Repositorios Guarantor
// Testar
// Criar unit tests
// Merge com release-1.2.1


// ====== NOVA BRANCH -> features/estate-contracts ======