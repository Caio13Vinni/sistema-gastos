# Sistema de Gastos Residenciais — Front-end

Front-end em React + TypeScript (Vite) que consome a API do back-end .NET.

## Como rodar

1. Instale as dependências:
   ```bash
   npm install
   ```

2. Copie o arquivo de variáveis de ambiente e ajuste a porta da API se necessário:
   ```bash
   cp .env.example .env
   ```
   Por padrão aponta para `http://localhost:5239/api` — confira a porta exibida pelo
   `dotnet run` do back-end e ajuste `VITE_API_URL` no `.env` se for diferente.

3. Suba o back-end (`Backend/SistemaGastos.API`) antes de iniciar o front-end.

4. Rode o front-end:
   ```bash
   npm run dev
   ```

5. Acesse `http://localhost:5173`.

## Estrutura

- `src/types.ts` — tipos TypeScript espelhando os DTOs da API.
- `src/api/client.ts` — funções de chamada HTTP (fetch) para cada endpoint.
- `src/components/` — `PessoaForm`, `PessoaList`, `TransacaoForm`, `TransacaoList`, `TotaisView`.
- `src/App.tsx` — orquestra o carregamento de dados e a navegação por abas
  (Pessoas / Transações / Totais).

## Funcionalidades cobertas

- Cadastro, listagem e remoção de pessoas (com aviso de que a remoção apaga
  as transações da pessoa em cascata).
- Cadastro e listagem de transações, com aviso visual quando a pessoa
  selecionada é menor de idade (só despesas são permitidas — validado também
  no backend).
- Consulta de totais por pessoa e total geral (receitas, despesas, saldo).
