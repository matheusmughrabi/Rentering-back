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

/* PRÓXIMA SPRINT - PLANEJAMENTO GERAL
    -> Geral
      1 - Criar plano de pagamento e plano free  OK 
      2 - Criar página home OK
      3 - Campo CreateDate em Entity OK

    -> Accounts
      1 - Implementar hash de password OK
      2 - Refresh token OK
      3 - Estudar se no front é possível verificar se o token é válido ou não OK
      4 - Definir se vale a pena utilizar Asp Net Identity OK

    -> Contracts
      1 - Refatoração do back utilizando o mesmo esquema de Corporations (algumas adaptações no front)

    -> Corporations
      1 - Registrar cada lucro individual do mês e só então fechar o mês OK
      2 - Descrição em participant balance OK
      3 - Ao ativar corporação, é preciso remover os participantes que tiverem recusado participação OK
      4 - Analisar possível troca de mês para período OK
      5 - Ordenar corporações da mais nova para a mais antiga OK
      6 - Ordenar mês do mais novo para o mais antigo OK

    -> Correções
      1 - Loading login mal-sucedido OK
 */

/* CRONOGRAMA
    -> Quinta-feira 05/08/2021
      - Criar campo CreateDate em Entity OK
      - Implementar hash de password OK
      - Refresh token OK
      - Estudar se no front é possível verificar se o token é válido ou não OK
      - Definir se vale a pena utilizar Asp Net Identity OK

    -> Sexta-feira 06/08/2021
      - Ordenar mês do mais novo para o mais antigo OK
      - Ordenar corporações da mais nova para a mais antiga OK
      - Descrição em participant balance OK
      - Ao ativar corporação, é preciso remover os participantes que tiverem recusado participação OK
      - Analisar possível troca de mês para período OK

    -> Sábado 07/08/2021
      - Registrar cada lucro individual do mês e só então fechar o mês OK
      - Melhorar visualização da corporação OK
      - Criar plano de pagamento e plano free ON GOING

    -> Domingo 08/08/2021
      - Criar página Home
      - Loading login mal-sucedido

    -> Segunda-feira 09/08/2021
      - Refatoração do back utilizando o mesmo esquema de Corporations (algumas adaptações no front)
      - Refatorar front principalmente com componentização

    -> Terça-feira 10/08/2021
      - Smoke-test e melhorias
      - HttpGets precisam de validação para saber se o usuário tem acesso ao dado solicitado
 */


