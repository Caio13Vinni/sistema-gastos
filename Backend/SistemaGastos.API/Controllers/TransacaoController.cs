using Microsoft.AspNetCore.Mvc;
using SistemaGastos.API.Dtos;
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
    public async Task<ActionResult<TransacaoResponseDto>> Criar([FromBody] CriarTransacaoRequest request)
    {
        try
        {
            var transacao = await _service.CriarTransacao(
                request.Descricao,
                request.Valor,
                request.Tipo,
                request.PessoaId
            );

            var dto = TransacaoResponseDto.FromEntity(transacao);
            return CreatedAtAction(nameof(Listar), new { id = transacao.Id }, dto);
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
    public async Task<ActionResult<List<TransacaoResponseDto>>> Listar()
    {
        var transacoes = await _service.ListarTransacoes();
        return Ok(transacoes.Select(TransacaoResponseDto.FromEntity).ToList());
    }

    [HttpGet("pessoa/{pessoaId}")]
    public async Task<ActionResult<List<TransacaoResponseDto>>> ListarPorPessoa(int pessoaId)
    {
        try
        {
            var transacoes = await _service.ListarTransacoesPorPessoa(pessoaId);
            return Ok(transacoes.Select(TransacaoResponseDto.FromEntity).ToList());
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }

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
    public async Task<ActionResult<TransacaoResponseDto>> Atualizar(int id, [FromBody] AtualizarTransacaoRequest request)
    {
        try
        {
            var transacao = await _service.AtualizarTransacao(id, request.Descricao, request.Valor, request.Tipo);
            return Ok(TransacaoResponseDto.FromEntity(transacao));
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