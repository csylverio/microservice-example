# Microservice Example
Exemplo de uma aplicação utilizando microserviços

## Objetivos
Este projeto tem como objetivo ensinar o desenvolvimento de uma aplicação utilizando a arquitetura de microserviços em C#. Para isso, serão implementados quatro microserviços, cada um com responsabilidades bem definidas e interações estabelecidas por meio de contextos delimitados (Bounded Contexts) seguindo os princípios do Domain-Driven Design (DDD). A abordagem visa promover a modularidade, escalabilidade e manutenibilidade do sistema, além de proporcionar uma compreensão prática de como integrar e gerenciar microserviços em um ambiente real.

A solução proposta será um Sistema Bancário Digital com as seguintes funcionalidades:
- **Serviço de Cadastro de Clientes**: Gerencia as informações dos clientes.
- **Serviço de Conta Corrente**: Processa transações financeiras.
- **Serviço de Notificações**: Envia alertas de saldo e movimentações.
- **Serviço de Empréstimos**: Calcula e gerencia propostas de crédito.

### Estrutura de Diretórios
``` 
BankingSystem/
 ├── docker-compose.yml          # Arquivo para orquestrar os serviços no Docker
 ├── Services/                   # Diretório de microserviços
 │    ├── CustomerService/       # Serviço de Cadastro de Clientes
 │    │    ├── CustomerService.Api/          # Projeto Web API
 |    |    ├── CustomerService.Application/  # Lógica de Aplicação
 │    │    ├── CustomerService.Domain/       # Lógica de Domínio
 │    │    └── CustomerService.Infrastructure/ # Repositórios e Acesso a Dados
 │    ├── AccountService/        # Serviço de Conta Corrente
 │    │    ├── AccountService.Api/
 |    |    ├── AccountService.Application/  
 │    │    ├── AccountService.Domain/
 │    │    └── AccountService.Infrastructure/
 │    ├── NotificationService/   # Serviço de Notificações
 │    │    ├── NotificationService.Api/
 |    |    ├── NotificationService.Application/  
 │    │    ├── NotificationService.Domain/
 │    │    └── NotificationService.Infrastructure/
 │    └── LoanService/           # Serviço de Empréstimos
 │         ├── LoanService.Api/
 |    |    ├── LoanService.Application/  
 │         ├── LoanService.Domain/
 │         └── LoanService.Infrastructure/
 └── Infrastructure/             # Código compartilhado entre serviços
      └── Shared/                # Classes e bibliotecas reutilizáveis
           ├── SharedKernel/     # Lógica de Domínio Compartilhada
           └── Utilities/        # Helpers e utilitários comuns

``` 


### **Arquitetura**

- **Arquitetura do software:** Microserviços
- **Padrão arquitetural:** DDD, Layered architecture, Event-Driven Architecture
- **Comunicação:**
    - Sincrona: HTTP
    - Assíncrona: RabbitMQ para eventos de transação e notificações
- **Persistência:**
    - Cadastro de Clientes: SQL Server
    - Transações e Notificações: MongoDB
    - Propostas de Empréstimo: SQL Server

### **Divisão dos Serviços**

1. **Serviço de Cadastro de Clientes**
    - Endpoints: Cadastro, Consulta, Atualização e Exclusão de Clientes
    - Persistência: SQL Server
2. **Serviço de Conta Corrente**
    - Endpoints: Consultar Saldo, Processar Depósito, Saque, Transferência entre contas
    - Persistência: MongoDB
    - Publicação de eventos de transação
3. **Serviço de Notificações**
    - Escuta eventos de transação do RabbitMQ
    - Envia notificações por e-mail ou SMS
4. **Serviço de Empréstimos**
    - Simulação de crédito
    - Aprovação ou rejeição de propostas
    - Persistência: SQL Server


    ## Ponto de atentação
    Este projeto tem como objetivo demonstrar as principais características de uma arquitetura de microserviços.
    
    Vale ressaltar que nem todos os componentes do projeto foram desenvolvidos seguindo critérios e requisitos técnicos essenciais para um sistema robusto.
    
    Haverá falhas na estrutura de diretório e padrão arquitetural dos micros serviços, ausência logging, monitoramento, tratamento de exceções e outros requisitos não funcionais, não foram contemplados nesta versão do projeto.