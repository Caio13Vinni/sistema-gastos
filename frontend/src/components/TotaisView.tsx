import type { RelatorioTotais } from "../types";

interface Props {
  relatorio: RelatorioTotais;
}

const formatadorMoeda = new Intl.NumberFormat("pt-BR", {
  style: "currency",
  currency: "BRL",
});


export function TotaisView({ relatorio }: Props) {
  return (
    <div className="card">
      <h2 className="card-title">Consulta de totais</h2>

      {relatorio.pessoas.length === 0 ? (
        <p className="texto-vazio">Nenhuma pessoa cadastrada ainda.</p>
      ) : (
        <table className="tabela-totais">
          <thead>
            <tr>
              <th>Pessoa</th>
              <th className="col-numero">Receitas</th>
              <th className="col-numero">Despesas</th>
              <th className="col-numero">Saldo</th>
            </tr>
          </thead>
          <tbody>
            {relatorio.pessoas.map((p) => (
              <tr key={p.pessoaId}>
                <td>{p.nome}</td>
                <td className="col-numero valor-receita">{formatadorMoeda.format(p.totalReceitas)}</td>
                <td className="col-numero valor-despesa">{formatadorMoeda.format(p.totalDespesas)}</td>
                <td className={`col-numero valor-saldo ${p.saldo < 0 ? "negativo" : ""}`}>
                  {formatadorMoeda.format(p.saldo)}
                </td>
              </tr>
            ))}
          </tbody>
          <tfoot>
            <tr>
              <td>Total geral</td>
              <td className="col-numero valor-receita">
                {formatadorMoeda.format(relatorio.totalGeralReceitas)}
              </td>
              <td className="col-numero valor-despesa">
                {formatadorMoeda.format(relatorio.totalGeralDespesas)}
              </td>
              <td className={`col-numero valor-saldo ${relatorio.saldoGeral < 0 ? "negativo" : ""}`}>
                {formatadorMoeda.format(relatorio.saldoGeral)}
              </td>
            </tr>
          </tfoot>
        </table>
      )}
    </div>
  );
}
