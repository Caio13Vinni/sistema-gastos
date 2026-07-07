namespace SistemaGastos.API.Dtos;

public class TotalPorPessoaDto
{
    public int PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }

    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }

    public decimal Saldo => TotalReceitas - TotalDespesas;
}

public class RelatorioTotaisDto
{
    public List<TotalPorPessoaDto> Pessoas { get; set; } = new();

    public decimal TotalGeralReceitas { get; set; }
    public decimal TotalGeralDespesas { get; set; }

    public decimal SaldoGeral => TotalGeralReceitas - TotalGeralDespesas;
}