import { useEffect, useState, useCallback } from "react";
import { listarPessoas, listarTransacoes, obterTotais, ApiError } from "./api/client";
import type { Pessoa, Transacao, RelatorioTotais } from "./types";
import { PessoaForm } from "./components/PessoaForm";
import { PessoaList } from "./components/PessoaList";
import { TransacaoForm } from "./components/TransacaoForm";
import { TransacaoList } from "./components/TransacaoList";
import { TotaisView } from "./components/TotaisView";
import { useTema } from "./useTema";
import "./App.css";

type Aba = "pessoas" | "transacoes" | "totais";

export default function App() {
  const [aba, setAba] = useState<Aba>("pessoas");
  const { tema, alternar } = useTema();

  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [relatorio, setRelatorio] = useState<RelatorioTotais | null>(null);
  const [carregando, setCarregando] = useState(true);
  const [erroCarregamento, setErroCarregamento] = useState<string | null>(null);

  // Carrega tudo de uma vez: mais simples que sincronizar estados parciais,
  // e o volume de dados de um sistema doméstico de gastos é pequeno.
  const recarregarTudo = useCallback(async () => {
    setErroCarregamento(null);
    try {
      const [pessoasResp, transacoesResp, relatorioResp] = await Promise.all([
        listarPessoas(),
        listarTransacoes(),
        obterTotais(),
      ]);
      setPessoas(pessoasResp);
      setTransacoes(transacoesResp);
      setRelatorio(relatorioResp);
    } catch (err) {
      setErroCarregamento(
        err instanceof ApiError
          ? err.message
          : "Não foi possível conectar à API. Verifique se o backend está rodando.",
      );
    } finally {
      setCarregando(false);
    }
  }, []);

  useEffect(() => {
    recarregarTudo();
  }, [recarregarTudo]);

  if (carregando) {
    return (
      <div className="app-shell">
        <p className="texto-vazio">Carregando…</p>
      </div>
    );
  }

  return (
    <div className="app-shell">
      <header className="app-header">
        <div className="app-header-topo">
          <div>
            <h1>Sistema de Gastos Residenciais</h1>
            <p>Controle de pessoas, transações e saldos da casa.</p>
          </div>
          <button
            className="botao-tema"
            onClick={alternar}
            aria-label={tema === "claro" ? "Ativar modo escuro" : "Ativar modo claro"}
          >
            {tema === "claro" ? "🌙 Escuro" : "☀️ Claro"}
          </button>
        </div>
      </header>

      {erroCarregamento && (
        <div className="banner-erro">
          {erroCarregamento}
          <button onClick={recarregarTudo}>Tentar de novo</button>
        </div>
      )}

      <nav className="abas">
        <button className={aba === "pessoas" ? "aba ativa" : "aba"} onClick={() => setAba("pessoas")}>
          Pessoas
        </button>
        <button className={aba === "transacoes" ? "aba ativa" : "aba"} onClick={() => setAba("transacoes")}>
          Transações
        </button>
        <button className={aba === "totais" ? "aba ativa" : "aba"} onClick={() => setAba("totais")}>
          Totais
        </button>
      </nav>

      <main className="conteudo">
        {aba === "pessoas" && (
          <div className="colunas">
            <PessoaForm
              onCriada={(pessoa) => {
                setPessoas((atual) => [...atual, pessoa]);
                recarregarTudo();
              }}
            />
            <PessoaList
              pessoas={pessoas}
              onDeletada={(id) => {
                setPessoas((atual) => atual.filter((p) => p.id !== id));
                setTransacoes((atual) => atual.filter((t) => t.pessoaId !== id));
                recarregarTudo();
              }}
            />
          </div>
        )}

        {aba === "transacoes" && (
          <div className="colunas">
            <TransacaoForm
              pessoas={pessoas}
              onCriada={(transacao) => {
                setTransacoes((atual) => [transacao, ...atual]);
                recarregarTudo();
              }}
            />
            <TransacaoList transacoes={transacoes} pessoas={pessoas} />
          </div>
        )}

        {aba === "totais" && relatorio && <TotaisView relatorio={relatorio} />}
      </main>
    </div>
  );
}
