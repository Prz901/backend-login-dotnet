# 🔐 API de Autenticação - ASP.NET Core + JWT + SQLite

Este projeto é uma API simples de autenticação utilizando **ASP.NET Core**, **Entity Framework Core com SQLite**, e autenticação baseada em **JWT (JSON Web Tokens)**. Ele permite o cadastro de usuários e login com geração de token JWT.

---

## 🚀 Como Rodar o Projeto

### 1. Clonar o Repositório

Clone este repositório para sua máquina local

### 2. Baixar as dependencias do dotnet usando o comando dotnet restore

### 3. Rodar o projeto com o dotnet run

-- Deixei uma documentacao junto com o Swagger mas as rotas que esse projeto possui são 

Post: "/api/auth/usuarios" - primeiro cria o usuario
Post: "/api/auth/login" -  efetua o login
Get: "/api/auth" - coloquei essa rota para teste de autenticação

### 4. Se quiser utilizar via postman vou deixar o Json de exemplo

{   
    "username": "peter",
    "password": "parker"
}

### 5. Estrutura do Projeto

├── Controllers/
│   └── AuthController.cs
├── Data/
│   └── UserDbContext.cs
├── Entities/
│   └── User.cs
├── Models/
│   └── UserDto.cs
├── Services/
│   └── AuthService.cs
├── Program.cs
├── appsettings.json
├── README.md

