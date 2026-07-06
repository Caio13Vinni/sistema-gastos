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

            // Retorna HTTP 201 (Created) com a transação criada
            return CreatedAtAction(nameof(Listar), new { id = transacao.Id }, transacao);
        }
        catch (KeyNotFoundException ex)
        {
            // Se o PessoaId não existir no banco, devolve HTTP 404 (Not Found)
            return NotFound(new { erro = ex.Message });
        }
        catch (ArgumentException ex)
        {
            // Se o tipo não for "Receita"/"Despesa", devolve HTTP 400 (Bad Request)
            return BadRequest(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            // Se tentar cadastrar RECEITA para MENOR DE IDADE (<18), devolve HTTP 400 (Bad Request)
            return BadRequest(new { erro = ex.Message });
        }
    }

    // GET /api/transacao -> Listar todas as transações
    [HttpGet]
    public async Task<ActionResult<List<Transacao>>> Listar()
    {
        var transacoes = await _service.ListarTransacoes();
        return Ok(transacoes); // Retorna HTTP 200 com a lista
    }
}

public class CriarTransacaoRequest
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty; // "Receita" ou "Despesa"
    public int PessoaId { get; set; }
}