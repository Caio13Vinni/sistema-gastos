
export type TipoTransacao = "Receita" | "Despesa";

export interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  pessoaId: number;
  dataDeCriacao: string; 
}

export interface Pessoa {
  id: number;
  nome: string;
  idade: number;
  dataDeCriacao: string;
  transacoes: Transacao[];
}

export interface TotalPorPessoa {
  pessoaId: number;
  nome: string;
  idade: number;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface RelatorioTotais {
  pessoas: TotalPorPessoa[];
  totalGeralReceitas: number;
  totalGeralDespesas: number;
  saldoGeral: number;
}

export interface ErroApi {
  erro: string;
}
