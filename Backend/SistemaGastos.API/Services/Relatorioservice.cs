using Microsoft.EntityFrameworkCore;
using SistemaGastos.API.Data;
using SistemaGastos.API.Dtos;
using SistemaGastos.API.Models;

namespace SistemaGastos.API.Services;

public class RelatorioService
{
    private readonly AppDbContext _context;

    public RelatorioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RelatorioTotaisDto> ObterTotais()
    {

        var pessoas = await _context.Pessoas
            .Select(p => new TotalPorPessoaDto
            {
                PessoaId = p.Id,
                Nome = p.Nome,
                Idade = p.Idade,
                TotalReceitas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => (decimal?)t.Valor) ?? 0m,
                TotalDespesas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => (decimal?)t.Valor) ?? 0m,
            })
            .OrderBy(p => p.Nome)
            .ToListAsync();

        var relatorio = new RelatorioTotaisDto
        {
            Pessoas = pessoas,
            TotalGeralReceitas = pessoas.Sum(p => p.TotalReceitas),
            TotalGeralDespesas = pessoas.Sum(p => p.TotalDespesas),
        };

        return relatorio;
    }
}