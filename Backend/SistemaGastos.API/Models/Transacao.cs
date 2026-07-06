public class Transacao
{
    public int ID { get; set; }
    public String Descricao { get; set; }
    public decimal Valor { get; set; }
    public String Tipo {get; set;}
    public int PessoaId { get; set; }

    //verificar quem fez a transacao 

    public Pessoa Pessoa {get; set;}
    public DateTime DataDeCriacao {get; set;}
}
