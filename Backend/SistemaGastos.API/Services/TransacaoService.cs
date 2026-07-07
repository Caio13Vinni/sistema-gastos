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

    public async Task<Transacao> CriarTransacao(string descricao, decimal valor, TipoTransacao tipo, int pessoaId)
    {

        var pessoa = await _context.Pessoas.FindAsync(pessoaId);
        if (pessoa == null)
        {
            throw new KeyNotFoundException("A pessoa informada não existe no cadastro.");
        }

        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException("A descrição é obrigatória.");
        }

        if (descricao.Length > 500)
        {
            throw new ArgumentException("A descrição não pode exceder 500 caracteres.");
        }

        if (valor <= 0)
        {
            throw new ArgumentException("O valor deve ser maior que zero.");
        }

        if (valor > 999999.99m)
        {
            throw new ArgumentException("O valor é muito grande.");
        }

        if (pessoa.Idade < 18 && tipo == TipoTransacao.Receita)
        {
            throw new InvalidOperationException(
                "Menores de idade (abaixo de 18 anos) só podem registrar despesas, não receitas.");
        }

        var transacao = new Transacao
        {
            Descricao = descricao.Trim(),
            Valor = valor,
            Tipo = tipo,  // ✅ Agora é Enum
            PessoaId = pessoaId,
            DataDeCriacao = DateTime.UtcNow  // ✅ UtcNow ao invés de Now
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        return transacao;
    }

    public async Task<List<Transacao>> ListarTransacoes()
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .OrderByDescending(t => t.DataDeCriacao)  // ✅ Ordenar por data
            .ToListAsync();
    }

    public async Task<List<Transacao>> ListarTransacoesPorPessoa(int pessoaId)
    {
        var pessoa = await _context.Pessoas.FindAsync(pessoaId);
        if (pessoa == null)
        {
            throw new KeyNotFoundException("A pessoa informada não existe no cadastro.");
        }

        return await _context.Transacoes
            .Where(t => t.PessoaId == pessoaId)
            .Include(t => t.Pessoa)
            .OrderByDescending(t => t.DataDeCriacao)
            .ToListAsync();
    }

    public async Task DeletarTransacao(int id)
    {
        var transacao = await _context.Transacoes.FindAsync(id);
        if (transacao == null)
        {
            throw new KeyNotFoundException("Transação não encontrada.");
        }

        _context.Transacoes.Remove(transacao);
        await _context.SaveChangesAsync();
    }

    public async Task<Transacao> AtualizarTransacao(int id, string descricao, decimal valor, TipoTransacao tipo)
    {
        var transacao = await _context.Transacoes.FindAsync(id);
        if (transacao == null)
        {
            throw new KeyNotFoundException("Transação não encontrada.");
        }

        if (string.IsNullOrWhiteSpace(descricao))
        {
            throw new ArgumentException("A descrição é obrigatória.");
        }

        if (valor <= 0)
        {
            throw new ArgumentException("O valor deve ser maior que zero.");
        }

        transacao.Descricao = descricao.Trim();
        transacao.Valor = valor;
        transacao.Tipo = tipo;

        _context.Transacoes.Update(transacao);
        await _context.SaveChangesAsync();

        return transacao;
    }
}