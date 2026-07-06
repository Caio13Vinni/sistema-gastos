public class Pessoa
{
    public int Id{ get; set;}
    public String Nome { get; set; }
    public int Idade { get; set; }

    public List<Transacao> Transacaos {get; set;}

    public DateTime DataDeCriacao {get; set;}

}