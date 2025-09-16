[Voltar para o README](../README.md)

# Terra Média - Backend

Este projeto é a parte de backend de uma aplicação inspirada no universo da Terra-Média, das obras de J. R. R. Tolkien. O backend permite gerenciar as interações com os dados dos livros e autores, fornecendo as informações necessárias para o frontend.

## Tecnologias

- **.NET 8**
- **Entity Framework Core:** Para mapeamento objeto-relacional (ORM)
- **ASP.NET Core Web API:** Para criação da API
- **AutoMapper:** Para mapeamento de objetos entre diferentes camadas
- **Mediator:** Para implementar o padrão de mediador e desacoplar as camadas da aplicação
- **FluentValidation:** Para validação fluente de objetos e entradas na aplicação
- **XUnit / NUnit:** Para testes automatizados
- **NSubstitute:** Para criar mocks e stubs nos testes unitários
- **Faker:** Para gerar dados falsos úteis para testes e desenvolvimento
- **FluentAssertions:** Para facilitar as asserções em testes, com uma API fluente e legível
- **PostgreSQL:** Banco de dados relacional utilizado no projeto

## 🗂️ Estrutura de Pastas

```
backend/
│
├── src/                           # Código fonte da aplicação
│   ├── Desafio.TerraMedia.Application  # Lógica de aplicação
│   ├── Desafio.TerraMedia.Common      # Classes comuns/utilitárias
│   ├── Desafio.TerraMedia.Domain      # Lógica de domínio (entidades, repositórios)
│   ├── Desafio.TerraMedia.IoC         # Injeção de dependências
│   ├── Desafio.TerraMedia.ORM         # ORM (Entity Framework)
│   └── Desafio.TerraMedia.WebApi     # API (controllers e configuração)
│
├── tests/                           # Testes do projeto
│   ├── Desafio.TerraMedia.Functional   # Testes funcionais
│   ├── Desafio.TerraMedia.Integration  # Testes de integração
│   └── Desafio.TerraMedia.Unit        # Testes unitários
│
├── .editorconfig                    # Configurações de editor
└── Desafio.TerraMedia.sln            # Solução do Visual Studio
```

## Pré-requisitos

- **.NET 8**
- **PostgreSQL**
- **Visual Studio 2022 ou Visual Studio Code**

## Como rodar

```bash
cd backend
dotnet
dotnet run --project Desafio.TerraMedia.WebApi    # ou: executando pela sua IDE
# Acesse: http://localhost:5000/ (ou outra porta configurada)
```

## Migrations

- Verifique se as configurações de conexão com o banco estão corretas em `appsettings.json` ou variáveis de ambiente.
- Execute o comando para rodar as migrations

```bash
dotnet ef database update --project Desafio.TerraMedia.ORM
```

## Testes

- Para rodar os testes execute

```bash
dotnet test
```
