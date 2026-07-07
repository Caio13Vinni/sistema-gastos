using Microsoft.AspNetCore.Mvc;
using SistemaGastos.API.Models;
using SistemaGastos.API.Services;

namespace SistemaGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacaoController : ControllerBase
{
    private readonly TransacaoService _service;

    public TransacaoController(TransacaoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Transacao>> Criar([FromBody] CriarTransacaoRequest request)
    {
        try
        {
            var transacao = await _service.CriarTransacao(
                request.Descricao,
                request.Valor,
                request.Tipo,
                request.PessoaId
            );

            return CreatedAtAction(nameof(Listar), new { id = transacao.Id }, transacao);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Transacao>>> Listar()
    {
        var transacoes = await _service.ListarTransacoes();
        return Ok(transacoes);
    }

    [HttpGet("pessoa/{pessoaId}")]
    public async Task<ActionResult<List<Transacao>>> ListarPorPessoa(int pessoaId)
    {
        try
        {
            var transacoes = await _service.ListarTransacoesPorPessoa(pessoaId);
            return Ok(transacoes);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }

    // ✅ NOVO: Deletar
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        try
        {
            await _service.DeletarTransacao(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Transacao>> Atualizar(int id, [FromBody] AtualizarTransacaoRequest request)
    {
        try
        {
            var transacao = await _service.AtualizarTransacao(id, request.Descricao, request.Valor, request.Tipo);
            return Ok(transacao);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}

public class CriarTransacaoRequest
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int PessoaId { get; set; }
}

public class AtualizarTransacaoRequest
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
}