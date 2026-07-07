using Microsoft.EntityFrameworkCore;
using SistemaGastos.API.Data;
using SistemaGastos.API.Models;

namespace SistemaGastos.API.Services;

public class PessoaService
{
    private readonly AppDbContext _context;
    public PessoaService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Pessoa> CriarPessoa(string nome, int idade)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome é obrigatório.");
        }

        if (idade < 0)
        {
            throw new ArgumentException("A idade não pode ser negativa.");
        }

        var pessoa = new Pessoa
        {
            Nome = nome,
            Idade = idade,
            DataDeCriacao = DateTime.Now
        };

        _context.Pessoas.Add(pessoa);

        await _context.SaveChangesAsync();

        return pessoa;
    }

    public async Task<List<Pessoa>> ListarPessoas()
    {
  
        return await _context.Pessoas
            .Include(p => p.Transacoes)
            .ToListAsync();
    }

    public async Task DeletarPessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);

        if (pessoa == null)
        {
            throw new KeyNotFoundException("Pessoa não encontrada.");
        }

        _context.Pessoas.Remove(pessoa);

        await _context.SaveChangesAsync();
    }
}