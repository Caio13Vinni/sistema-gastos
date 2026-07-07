using Microsoft.EntityFrameworkCore;
using SistemaGastos.API.Data;
using SistemaGastos.API.Models;

namespace SistemaGastos.API.Services;

public class TransacaoService
{
    private readonly AppDbContext _context;

    public TransacaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transacao> CriarTransacao(string descricao, decimal valor, string tipo, int pessoaId)
    {
        var pessoa = await _context.Pessoas.FindAsync(pessoaId);
        if (pessoa == null)
        {
            throw new KeyNotFoundException("A pessoa informada não existe no cadastro.");
        }

        if (!tipo.Equals("Receita", StringComparison.OrdinalIgnoreCase) && 
            !tipo.Equals("Despesa", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("O tipo da transação deve ser exclusivamente 'Receita' ou 'Despesa'.");
        }

        var tipoPadronizado = tipo.Equals("Receita", StringComparison.OrdinalIgnoreCase) ? "Receita" : "Despesa";


        if (pessoa.Idade < 18 && tipoPadronizado == "Receita")
        {
            throw new InvalidOperationException("Menores de idade (abaixo de 18 anos) só podem registrar despesas, não receitas.");
        }

        var transacao = new Transacao
        {
            Descricao = descricao,
            Valor = valor,
            Tipo = tipoPadronizado,
            PessoaId = pessoaId,
            DataDeCriacao = DateTime.Now
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        return transacao;
    }

    public async Task<List<Transacao>> ListarTransacoes()
    {

        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .ToListAsync();
    }
}