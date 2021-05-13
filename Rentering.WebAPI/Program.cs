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
     -> Passar storedProcedures para o C# OK
     -> Criar uma consulta específica para cada cenário OK
     -> Com relação a etapa anterior, uma das consultas de contratos por exemplo terá que alimentar a entidade com seus campos e também com a lista de pagamentos OK
     -> Com relação aos repositórios Query, é preciso trazer os dados de maneira mais otimizada. Para isso os dados serão filtrados com Where e também serão utilizados os devidos Select. Nota-se também que será necessário criar um GetQueryResult específico para cada cenário, apenas com os campos de interesse OK
     -> Testar tudo OK
 */

// POSSIBILIDADES ADICIONAIS -> Até Domingo à noite (16/05/2021)
/*
  -> Criar tabela intermediária com os campos AccountId, ContractId e UserRoleInTheContractId 
     (Owner, Participant OU ENTÃO Owner, Renter, Tenant and Guarantor)
  -> Renter, Tenant e Guarantor não terão mais o campo AccountId, mas sim o campo ContractId 
  -> Refatorar nomenclatura para EstateContract OK

  -> Criar AccountContractsEntity (Id, AccountId, ContractId, ParticipantRole) OK
  -> Criar IReadOnlyCollection de cada participante e também alterar cada entity de participante para ter ContractId
  -> Refatorar nos locais que forem necessários do código
  -> Refatorar Migrations existentes
  -> Criar Migration de AccountContracts Table 
 */

// AUTENTICAÇÃO -> Até Terça-feira à noite (18/05/2021)
// Criar auth commands, IService e handlers
// Implementar Service

// TESTES DE UNIDADE -> Até sexta-feira à noite (21/05/2021)
// Criar Account UnitTests -> Entity, Handlers
// Criar Renter UnitTests -> Entity, Handlers, AuthHandlers e AuthService
// Criar Tenant UnitTests -> Entity, Handlers, AuthHandlers e AuthService
// Criar Guarantor UnitTests -> Entity, Handlers, AuthHandlers e AuthService
// Criar EstateContract UnitTests -> Entity, Handlers, AuthHandlers e AuthService
// Criar ContractPayment UnitTests -> Entity, Handlers, AuthHandlers e AuthService

// TESTE E MERGE -> Até sábado à noite (22/05/2021)
// Testar módulo de contratos inteiro -> Relizar teste que possam gerar exceções para ver se o sistema está robusto
// Revisão Geral
// Mergear e liberar versão 1.2.1 da API

// DATA FINAL -> 22/05/2021


