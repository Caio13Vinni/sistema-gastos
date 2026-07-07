import type { Pessoa, Transacao } from "../types";

interface Props {
  transacoes: Transacao[];
  pessoas: Pessoa[];
}

const formatadorMoeda = new Intl.NumberFormat("pt-BR", {
  style: "currency",
  currency: "BRL",
});

const formatadorData = new Intl.DateTimeFormat("pt-BR", {
  day: "2-digit",
  month: "2-digit",
  year: "numeric",
  hour: "2-digit",
  minute: "2-digit",
});

export function TransacaoList({ transacoes, pessoas }: Props) {
  const nomePorPessoaId = new Map(pessoas.map((p) => [p.id, p.nome]));

  return (
    <div className="card">
      <h2 className="card-title">Transações</h2>
      {transacoes.length === 0 ? (
        <p className="texto-vazio">Nenhuma transação cadastrada ainda.</p>
      ) : (
        <ul className="ledger">
          {transacoes.map((t) => (
            <li key={t.id} className="linha-ledger">
              <div className="ledger-info">
                <span className="ledger-descricao">{t.descricao}</span>
                <span className="ledger-meta">
                  {nomePorPessoaId.get(t.pessoaId) ?? `Pessoa #${t.pessoaId}`} ·{" "}
                  {formatadorData.format(new Date(t.dataDeCriacao))}
                </span>
              </div>
              <span className={t.tipo === "Receita" ? "ledger-valor receita" : "ledger-valor despesa"}>
                {t.tipo === "Receita" ? "+ " : "− "}
                {formatadorMoeda.format(t.valor)}
              </span>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
