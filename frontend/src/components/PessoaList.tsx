import { useState } from "react";
import { deletarPessoa, ApiError } from "../api/client";
import type { Pessoa } from "../types";

interface Props {
  pessoas: Pessoa[];
  onDeletada: (id: number) => void;
}


export function PessoaList({ pessoas, onDeletada }: Props) {
  const [erro, setErro] = useState<string | null>(null);
  const [removendoId, setRemovendoId] = useState<number | null>(null);

  async function handleDeletar(pessoa: Pessoa) {
    const confirmado = window.confirm(
      `Remover ${pessoa.nome}? Todas as transações dessa pessoa também serão apagadas.`,
    );
    if (!confirmado) return;

    setErro(null);
    setRemovendoId(pessoa.id);
    try {
      await deletarPessoa(pessoa.id);
      onDeletada(pessoa.id);
    } catch (err) {
      setErro(err instanceof ApiError ? err.message : "Não foi possível remover a pessoa.");
    } finally {
      setRemovendoId(null);
    }
  }

  return (
    <div className="card">
      <h2 className="card-title">Pessoas cadastradas</h2>
      {erro && <p className="mensagem-erro">{erro}</p>}
      {pessoas.length === 0 ? (
        <p className="texto-vazio">Nenhuma pessoa cadastrada ainda.</p>
      ) : (
        <ul className="lista-pessoas">
          {pessoas.map((pessoa) => (
            <li key={pessoa.id} className="linha-pessoa">
              <div>
                <span className="nome-pessoa">{pessoa.nome}</span>
                <span className="detalhe-pessoa">
                  {pessoa.idade} anos · {pessoa.transacoes.length}{" "}
                  {pessoa.transacoes.length === 1 ? "transação" : "transações"}
                  {pessoa.idade < 18 && <span className="etiqueta-menor"> menor de idade</span>}
                </span>
              </div>
              <button
                className="botao-remover"
                onClick={() => handleDeletar(pessoa)}
                disabled={removendoId === pessoa.id}
              >
                {removendoId === pessoa.id ? "Removendo…" : "Remover"}
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
