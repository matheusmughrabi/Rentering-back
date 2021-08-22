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

/* Pontos de corre��o
    -> Common.Shared
      - Deletar SafeEnums OK
      - Deletar construtores desnecess�rios OK
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


/* PR�XIMA SPRINT -> 
    - Criar descri��o no front na parte de trocar de licensa de maneira que indique que � apenas um pagamento fict�cio
    - HttpGets precisam de valida��o para saber se o usu�rio tem acesso ao dado solicitado 
    - Smoke-test e melhorias
    - Criar p�gina Home 
    - Loading login mal-sucedido 
 */




