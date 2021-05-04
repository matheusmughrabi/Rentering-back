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
// Adicionar validações nos ValueObjects OK
// Spouse atributos nao precisa se não for casado OK
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
// Testar ON GOING -> Corrigir bugs em renterService e tenantService, além disso é preciso criar testes para estes services OK
// Criar query repositories e controllers
// Testar
// Merge com release-1.2.1


// ====== NOVA BRANCH -> feature/guarantor-functionality ======
// Criar handlers OK
// Criar unit tests OK
// Criar auth handlers OK
// Criar auth unit tests OK
// Implementar GuarantorService OK
// Criar unit tests para guarantorService OK
// Criar tabela Guarantor OK
// Criar Stored Procedures Guarantor OK
// Implementar Repositorios Guarantor OK
// Criar controller OK
// Testar OK
// Merge com release-1.2.1 OK


// ====== NOVA BRANCH -> features/estate-contracts ======
// Criar ContractWithGuarantorEntity OK
// Criar testes de unidade OK

// Criar migration de tabela ContractsWithGuarantor OK
// Criar StoredProcedures OK

// Criar commands, IRepository e handlers ON GOING
// Criar testes de unidade ON GOING
// Implementar CUDRepository ON GOING

// OBS:
// 1) Precisa incluir campo de status do Renter, Tenant e Guarantor nas tabelas do BD OK
// 2) Precisa garantir que eles já não estejam associados a outro contrato OK

// Criar auth commands, IService e handlers
// Criar testes de unidade para auth handlers

// Implementar Service
// Criar testes de unidade para Service

// Implementar Controller
// Testar
