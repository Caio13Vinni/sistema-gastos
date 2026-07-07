import { useState, type FormEvent } from "react";
import { criarPessoa, ApiError } from "../api/client";
import type { Pessoa } from "../types";

interface Props {
  onCriada: (pessoa: Pessoa) => void;
}

export function PessoaForm({ onCriada }: Props) {
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [erro, setErro] = useState<string | null>(null);
  const [enviando, setEnviando] = useState(false);

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setErro(null);

    const idadeNumero = Number(idade);
    if (!nome.trim() || idade.trim() === "" || Number.isNaN(idadeNumero)) {
      setErro("Preencha o nome e uma idade válida.");
      return;
    }

    setEnviando(true);
    try {
      const pessoa = await criarPessoa(nome.trim(), idadeNumero);
      onCriada(pessoa);
      setNome("");
      setIdade("");
    } catch (err) {
      setErro(err instanceof ApiError ? err.message : "Não foi possível cadastrar a pessoa.");
    } finally {
      setEnviando(false);
    }
  }

  return (
    <form className="card form" onSubmit={handleSubmit}>
      <h2 className="card-title">Nova pessoa</h2>
      <div className="field-row">
        <label className="field">
          <span>Nome</span>
          <input
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            placeholder="Ex: Ana Ferreira"
            maxLength={200}
          />
        </label>
        <label className="field field-narrow">
          <span>Idade</span>
          <input
            value={idade}
            onChange={(e) => setIdade(e.target.value)}
            placeholder="Ex: 30"
            inputMode="numeric"
          />
        </label>
      </div>
      {erro && <p className="mensagem-erro">{erro}</p>}
      <button type="submit" disabled={enviando}>
        {enviando ? "Cadastrando…" : "Cadastrar pessoa"}
      </button>
    </form>
  );
}
