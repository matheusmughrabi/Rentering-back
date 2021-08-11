# Rentering back

## Descrição
O projeto Rentering possibilita o gerenciamento de pagamentos com foco em dois cenários principais divididos nos módulos Contracts e Corporations. Ambos os módulos estão em constante melhoria, tanto do ponto de visto das regras de negócio como da qualidade do código. Além disso, outros módulos estão em fase de planejamento e serão desenvolvidos brevemente para atender uma maior diversidade de cenários.

O primeiro cenário foi implementado dentro do módulo Contracts e foca em resolver o problema de gerenciar pagamentos mensais de contratos (imobiliários, por exemplo). Neste módulo, o usuário poderá criar contratos e convidar participantes, onde cada participante terá o seu papel dentro do contrato. Os participantes podem realizar pagamentos, que podem ser aceitos ou recusados pelas outras partes do contrato.

Já o módulo Corporations modela a situação onde uma empresa formada por sócios possui receitas mensais que precisam divididas entre seus associados. O usuário pode criar corporações, nas quais será o administrador e convidar participantes para serem seus sócios. Uma vez que o convite for enviado aos participantes, os mesmos podem aceitar ou recusar a participação na corporação. Cada participante possui sua porcentagem sobre os lucros da corporação que são divididos em períodos. Uma vez que a corporação for ativada, o administrador poderá adicionar períodos, onde cada período possui suas entradas de receita. Ao final do período, o administrador poderá fazer o fechamento do mês e os ganhos de cada participante serão calculados. O participante poderá então aceitar ou contestar o período caso entenda que o mesmo possui alguma divergência nas receitas por exemplo.

## Executando o projeto
- Baixar o repositório Rentering-back
- Trocar a string de conexão no appSettings.js do projeto Rentering.WebAPI
- Executar update-database Rentering.Infra
- Para executar em conjunto com o front-end, baixar o repositório Rentering-front e executar o comando ng-serve

## Requisitos mínimos para executar o projeto
- .NET 5
- SqlServer
- Angular 12

## Observação
O projeto está aberto e todos que tiverem serão interesse em contribuir com pull-requests serão muito bem vindos!!

## Autores

Matheus Campanini Mughrabi
