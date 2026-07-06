namespace SistemaGastos.API.Models;

public class Transacao
{
    public int Id { get; set; }
    public String Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public String Tipo {get; set;} = string.Empty;
    public int PessoaId { get; set; }

    //verificar quem fez a transacao 

    public Pessoa? Pessoa {get; set;}
    public DateTime DataDeCriacao {get; set;}
}
