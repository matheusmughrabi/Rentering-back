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

// Criar commands, IRepository e handlers OK
// Implementar CUDRepository OK
// Implementar QueryRepository OK

// OBS:
// 1) Precisa incluir campo de status do Renter, Tenant e Guarantor nas tabelas do BD OK
// 2) Precisa garantir que eles já não estejam associados a outro contrato OK

// Implementar InviteRenter OK
// Implementar InviteTenant OK
// Implementar InviteGuarantor OK

// Estudar implementações de UnitOfWork com dapper OK
// Implementar ContractUnitOfWork OK
// Implementar AccountUnitOfWork OK

// Modelar melhor como será feita a criação de ciclos de contrato OK
// Implementar nova modelagem OK
// Implementar ContractPaymentController OK
// ExecutePayment, Accept e Reject OK

/* Refatoração dos repositórios 
     -> Passar storedProcedures para o C# ON GOING
     -> Criar uma consulta específica para cada cenário
     -> Com relação a etapa anterior, uma das consultas de contratos por exemplo terá que alimentar a entidade com seus campos e também com a lista de pagamentos
     -> Com relação aos repositórios Query, é preciso trazer os dados de maneira mais otimizada. Para isso os dados serão filtrados com Where e também serão utilizados os devidos Select. Nota-se também que será necessário criar um GetQueryResult específico para cada cenário, apenas com os campos de interesse
 */

// Criar auth commands, IService e handlers
// Criar testes de unidade para auth handlers
// Implementar Service

// Criar todos os testes de unidade
// Testar módulo de contratos inteiro
// Mergear e liberar nova versão
