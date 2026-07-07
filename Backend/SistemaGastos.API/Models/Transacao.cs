namespace SistemaGastos.API.Models;

public enum TipoTransacao
{
    Receita = 1,
    Despesa = 2
}

public class Transacao
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int PessoaId { get; set; }
    public Pessoa? Pessoa { get; set; }
    public DateTime DataDeCriacao { get; set; }
}