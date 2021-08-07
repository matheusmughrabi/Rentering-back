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

/* PR�XIMA SPRINT - PLANEJAMENTO GERAL
    -> Geral
      1 - Criar plano de pagamento e plano free  OK 
      2 - Criar p�gina home OK
      3 - Campo CreateDate em Entity OK

    -> Accounts
      1 - Implementar hash de password OK
      2 - Refresh token OK
      3 - Estudar se no front � poss�vel verificar se o token � v�lido ou n�o OK
      4 - Definir se vale a pena utilizar Asp Net Identity OK

    -> Contracts
      1 - Refatora��o do back utilizando o mesmo esquema de Corporations (algumas adapta��es no front)

    -> Corporations
      1 - Registrar cada lucro individual do m�s e s� ent�o fechar o m�s OK
      2 - Descri��o em participant balance OK
      3 - Ao ativar corpora��o, � preciso remover os participantes que tiverem recusado participa��o OK
      4 - Analisar poss�vel troca de m�s para per�odo OK
      5 - Ordenar corpora��es da mais nova para a mais antiga OK
      6 - Ordenar m�s do mais novo para o mais antigo OK

    -> Corre��es
      1 - Loading login mal-sucedido OK
 */

/* CRONOGRAMA
    -> Quinta-feira 05/08/2021
      - Criar campo CreateDate em Entity OK
      - Implementar hash de password OK
      - Refresh token OK
      - Estudar se no front � poss�vel verificar se o token � v�lido ou n�o OK
      - Definir se vale a pena utilizar Asp Net Identity OK

    -> Sexta-feira 06/08/2021
      - Ordenar m�s do mais novo para o mais antigo OK
      - Ordenar corpora��es da mais nova para a mais antiga OK
      - Descri��o em participant balance OK
      - Ao ativar corpora��o, � preciso remover os participantes que tiverem recusado participa��o OK
      - Analisar poss�vel troca de m�s para per�odo OK

    -> S�bado 07/08/2021
      - Registrar cada lucro individual do m�s e s� ent�o fechar o m�s OK
      - Melhorar visualiza��o da corpora��o OK
      - Criar plano de pagamento e plano free ON GOING

    -> Domingo 08/08/2021
      - Criar p�gina Home
      - Loading login mal-sucedido

    -> Segunda-feira 09/08/2021
      - Refatora��o do back utilizando o mesmo esquema de Corporations (algumas adapta��es no front)
      - Refatorar front principalmente com componentiza��o

    -> Ter�a-feira 10/08/2021
      - Smoke-test e melhorias
      - HttpGets precisam de valida��o para saber se o usu�rio tem acesso ao dado solicitado
 */


