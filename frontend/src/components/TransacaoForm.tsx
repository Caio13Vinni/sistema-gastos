import { useState, type FormEvent } from "react";
import { criarTransacao, ApiError } from "../api/client";
import type { Pessoa, Transacao, TipoTransacao } from "../types";

interface Props {
  pessoas: Pessoa[];
  onCriada: (transacao: Transacao) => void;
}

export function TransacaoForm({ pessoas, onCriada }: Props) {
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState<TipoTransacao>("Despesa");
  const [pessoaId, setPessoaId] = useState<string>("");
  const [erro, setErro] = useState<string | null>(null);
  const [enviando, setEnviando] = useState(false);

  const pessoaSelecionada = pessoas.find((p) => p.id === Number(pessoaId));
  const pessoaEhMenorDeIdade = (pessoaSelecionada?.idade ?? 0) < 18;

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setErro(null);

    const valorNumero = Number(valor.replace(",", "."));
    if (!descricao.trim() || !pessoaId || Number.isNaN(valorNumero) || valorNumero <= 0) {
      setErro("Preencha a descrição, um valor maior que zero e selecione uma pessoa.");
      return;
    }

    setEnviando(true);
    try {
      const transacao = await criarTransacao(descricao.trim(), valorNumero, tipo, Number(pessoaId));
      onCriada(transacao);
      setDescricao("");
      setValor("");
    } catch (err) {
      setErro(err instanceof ApiError ? err.message : "Não foi possível cadastrar a transação.");
    } finally {
      setEnviando(false);
    }
  }

  return (
    <form className="card form" onSubmit={handleSubmit}>
      <h2 className="card-title">Nova transação</h2>

      <label className="field">
        <span>Pessoa</span>
        <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)}>
          <option value="">Selecione…</option>
          {pessoas.map((p) => (
            <option key={p.id} value={p.id}>
              {p.nome} ({p.idade} anos)
            </option>
          ))}
        </select>
      </label>

      <div className="field-row">
        <label className="field">
          <span>Descrição</span>
          <input
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            placeholder="Ex: Supermercado"
            maxLength={500}
          />
        </label>
        <label className="field field-narrow">
          <span>Valor (R$)</span>
          <input
            value={valor}
            onChange={(e) => setValor(e.target.value)}
            placeholder="Ex: 150,00"
            inputMode="decimal"
          />
        </label>
      </div>

      <fieldset className="tipo-toggle">
        <legend>Tipo</legend>
        <label className={tipo === "Despesa" ? "opcao-tipo ativa" : "opcao-tipo"}>
          <input
            type="radio"
            name="tipo"
            checked={tipo === "Despesa"}
            onChange={() => setTipo("Despesa")}
          />
          Despesa
        </label>
        <label
          className={tipo === "Receita" ? "opcao-tipo ativa" : "opcao-tipo"}
          title={pessoaEhMenorDeIdade ? "Menores de idade só podem registrar despesas" : undefined}
        >
          <input
            type="radio"
            name="tipo"
            checked={tipo === "Receita"}
            disabled={pessoaEhMenorDeIdade}
            onChange={() => setTipo("Receita")}
          />
          Receita
        </label>
      </fieldset>
      {pessoaEhMenorDeIdade && (
        <p className="mensagem-aviso">Essa pessoa é menor de idade: só despesas podem ser cadastradas.</p>
      )}

      {erro && <p className="mensagem-erro">{erro}</p>}
      <button type="submit" disabled={enviando}>
        {enviando ? "Cadastrando…" : "Cadastrar transação"}
      </button>
    </form>
  );
}
