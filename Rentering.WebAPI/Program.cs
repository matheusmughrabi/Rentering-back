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

/* Pontos de correção
    -> Common.Shared
      - Deletar SafeEnums OK
      - Deletar construtores desnecessários OK
      - Corrigir classe ICommand OK
      - CreateDate private? ADIADO
      - Query results estrutura de pastas OK

   -> Corporation
     - Corrigir nomes dos enums para ENome OK
     - Verificar construtores de cada entity OK
     - Endpoints OK
     - Unit Tests

   -> Accounts
     - Corrigir nomes dos enums para ENome OK
     - Verificar construtores de cada entity
     - Endpoints
     - Query repositories
     - Unit Tests

   -> Contracts
     - Corrigir nomes dos enums para ENome OK
     - Verificar construtores de cada entity
     - Query repositories
     - Endpoints
     - Unit Tests
 */


/* PRÓXIMA SPRINT -> 
    - Criar descrição no front na parte de trocar de licensa de maneira que indique que é apenas um pagamento fictício
    - HttpGets precisam de validação para saber se o usuário tem acesso ao dado solicitado 
    - Smoke-test e melhorias
    - Criar página Home 
    - Loading login mal-sucedido 
 */




