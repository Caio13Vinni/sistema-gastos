using SistemaGastos.API.Models;

namespace SistemaGastos.API.Dtos;

/// <summary>
/// Representa uma transação na resposta da API.
/// Não inclui o objeto Pessoa completo (só o PessoaId), evitando
/// referência circular Pessoa -> Transacoes -> Pessoa.
/// </summary>
public class TransacaoResponseDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int PessoaId { get; set; }
    public DateTime DataDeCriacao { get; set; }

    public static TransacaoResponseDto FromEntity(Transacao t) => new()
    {
        Id = t.Id,
        Descricao = t.Descricao,
        Valor = t.Valor,
        Tipo = t.Tipo,
        PessoaId = t.PessoaId,
        DataDeCriacao = t.DataDeCriacao,
    };
}

/// <summary>
/// Representa uma pessoa na resposta da API, incluindo suas transações
/// já convertidas para <see cref="TransacaoResponseDto"/> (sem o campo Pessoa
/// aninhado em cada transação, evitando ciclo de referência).
/// </summary>
public class PessoaResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public DateTime DataDeCriacao { get; set; }
    public List<TransacaoResponseDto> Transacoes { get; set; } = new();

    public static PessoaResponseDto FromEntity(Pessoa p) => new()
    {
        Id = p.Id,
        Nome = p.Nome,
        Idade = p.Idade,
        DataDeCriacao = p.DataDeCriacao,
        Transacoes = p.Transacoes.Select(TransacaoResponseDto.FromEntity).ToList(),
    };
}