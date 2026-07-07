# 💰 Sistema de Controle de Gastos

Um sistema Full Stack para gerenciamento de gastos pessoais, desenvolvido para praticar conceitos modernos de desenvolvimento utilizando **ASP.NET Core** no backend e **React + TypeScript** no frontend.

O projeto permite cadastrar pessoas, registrar transações financeiras e visualizar relatórios para acompanhamento das movimentações.

---

# 📖 Sobre o projeto

O objetivo deste projeto é demonstrar conhecimentos em desenvolvimento Full Stack, aplicando boas práticas de organização de código, separação de responsabilidades e criação de APIs REST.

Durante o desenvolvimento foram utilizados conceitos como:

- Arquitetura em camadas
- API REST
- Entity Framework Core
- SQLite
- React
- TypeScript
- Consumo de API
- Tratamento global de exceções
- DTOs
- Migrations
- Organização de serviços

Este projeto faz parte do meu portfólio e demonstra minhas habilidades no desenvolvimento de aplicações web completas.

---

# 🛠 Tecnologias utilizadas

## Backend

- ASP.NET Core
- Entity Framework Core
- SQLite
- C#
- LINQ
- REST API

## Frontend

- React
- TypeScript
- Vite
- CSS

---

# 📂 Estrutura do projeto

```
Sistema-Gastos
│
├── Backend
│   └── SistemaGastos.API
│
└── frontend
```

### Backend

Responsável por:

- Cadastro de pessoas
- Cadastro de transações
- Geração de relatórios
- Persistência dos dados
- Regras de negócio

### Frontend

Responsável por:

- Interface do usuário
- Consumo da API
- Cadastro de dados
- Visualização das informações

---

# ⚙️ Pré-requisitos

Antes de executar o projeto é necessário possuir instalado:

- .NET SDK
- Node.js
- npm

---

# 🚀 Como executar o projeto

## 1. Clone o repositório

```bash
git clone https://github.com/Caio13Vinni/sistema-gastos.git
```

Entre na pasta do projeto:

```bash
cd sistema-gastos
```

---

# ▶️ Executando o Backend

Entre na pasta da API:

```bash
cd Backend/SistemaGastos.API
```

Instale as dependências:

```bash
dotnet restore
```

Execute as migrations (caso necessário):

```bash
dotnet ef database update
```

Inicie a aplicação:

```bash
dotnet run
```

A API ficará disponível em:

```
http://localhost:5239
```

(dependendo da configuração do ambiente.)

---

# ▶️ Executando o Frontend

Abra outro terminal.

Entre na pasta:

```bash
cd frontend
```

Instale as dependências:

```bash
npm install
```

Configure o arquivo `.env`.

Caso ainda não exista:

```bash
cp .env.example .env
```

Inicie o projeto:

```bash
npm run dev
```

O frontend ficará disponível em:

```
http://localhost:5173
```

---

# 📊 Funcionalidades

- Cadastro de pessoas
- Cadastro de receitas
- Cadastro de despesas
- Listagem de transações
- Relatórios financeiros
- Integração entre frontend e backend
- Persistência em banco SQLite

---

# 🗄 Banco de dados

O projeto utiliza:

- SQLite
- Entity Framework Core
- Migrations

O banco é criado automaticamente através das migrations.

---

# 📌 Organização do Backend

```
Controllers
```

Responsáveis pelos endpoints da API.

```
Services
```

Contêm toda a regra de negócio da aplicação.

```
Models
```

Representam as entidades do banco de dados.

```
Dtos
```

Objetos utilizados para comunicação entre cliente e servidor.

```
Middleware
```

Responsável pelo tratamento global de exceções.

```
Data
```

Configuração do Entity Framework.

---

# 🔗 Endpoints

A API possui endpoints para:

- Pessoas
- Transações
- Relatórios

Todos organizados em controllers independentes seguindo o padrão REST.

---

# 📸 Demonstração

Em breve serão adicionadas imagens e GIFs demonstrando a aplicação em funcionamento.

---

# 🎯 Objetivo deste projeto

Este projeto foi desenvolvido para consolidar conhecimentos em:

- Desenvolvimento Full Stack
- ASP.NET Core
- React
- TypeScript
- APIs REST
- Entity Framework
- Organização de projetos
- Boas práticas de desenvolvimento

---

# 👨‍💻 Informações para recrutadores

Este projeto foi desenvolvido com foco em demonstrar conhecimentos práticos em desenvolvimento Full Stack.

Durante sua construção procurei aplicar princípios utilizados em projetos profissionais, como:

- Separação de responsabilidades
- Organização em camadas
- Código limpo
- Estrutura escalável
- Consumo de APIs REST
- Tratamento de exceções
- Persistência de dados utilizando Entity Framework

Caso queira avaliar rapidamente o projeto:

1. Execute o backend com `dotnet run`;
2. Execute o frontend com `npm run dev`;
3. Acesse a aplicação pelo navegador;
4. Navegue pelas funcionalidades de cadastro e gerenciamento de gastos.

Todo o código foi organizado pensando em facilitar manutenção, leitura e evolução futura.

---

# 🚀 Melhorias futuras

- Autenticação com JWT
- Dashboard com gráficos
- Categorias de gastos
- Filtros avançados
- Exportação para PDF
- Exportação para Excel
- Docker
- Deploy na nuvem
- Testes unitários
- Testes de integração

---

# 👤 Autor

**Caio Vinícius**

GitHub:

https://github.com/Caio13Vinni

---

⭐ Caso este projeto tenha sido útil ou interessante, deixe uma estrela no repositório.
