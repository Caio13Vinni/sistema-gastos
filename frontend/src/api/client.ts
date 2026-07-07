import type { Pessoa, RelatorioTotais, Transacao, TipoTransacao } from "../types";

const BASE_URL = import.meta.env.VITE_API_URL ?? "http://localhost:5239/api";


export class ApiError extends Error {}


async function request<T>(path: string, options?: RequestInit): Promise<T> {
  const response = await fetch(`${BASE_URL}${path}`, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });

  // 204 No Content (usado no DELETE) não tem corpo para parsear.
  if (response.status === 204) {
    return undefined as T;
  }

  const data = await response.json();

  if (!response.ok) {
    const mensagem = data?.erro ?? "Ocorreu um erro inesperado ao falar com o servidor.";
    throw new ApiError(mensagem);
  }

  return data as T;
}

// ---------- Pessoas ----------

export function listarPessoas(): Promise<Pessoa[]> {
  return request<Pessoa[]>("/pessoa");
}

export function criarPessoa(nome: string, idade: number): Promise<Pessoa> {
  return request<Pessoa>("/pessoa", {
    method: "POST",
    body: JSON.stringify({ nome, idade }),
  });
}

export function deletarPessoa(id: number): Promise<void> {
  return request<void>(`/pessoa/${id}`, { method: "DELETE" });
}

// ---------- Transações ----------

export function listarTransacoes(): Promise<Transacao[]> {
  return request<Transacao[]>("/transacao");
}

export function criarTransacao(
  descricao: string,
  valor: number,
  tipo: TipoTransacao,
  pessoaId: number,
): Promise<Transacao> {
  return request<Transacao>("/transacao", {
    method: "POST",
    body: JSON.stringify({ descricao, valor, tipo, pessoaId }),
  });
}

// ---------- Totais ----------

export function obterTotais(): Promise<RelatorioTotais> {
  return request<RelatorioTotais>("/relatorio/totais");
}
