using Microsoft.AspNetCore.Mvc;
using SistemaGastos.API.Dtos;
using SistemaGastos.API.Services;

namespace SistemaGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly PessoaService _service;

    public PessoaController(PessoaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<PessoaResponseDto>> Criar([FromBody] CriarPessoaRequest request)
    {
        try
        {
            var pessoa = await _service.CriarPessoa(request.Nome, request.Idade);
            // Retorna HTTP 201 
            return CreatedAtAction(nameof(Listar), new { id = pessoa.Id }, PessoaResponseDto.FromEntity(pessoa));
        }
        catch (ArgumentException ex)
        {
            // devolve HTTP 400 pra erro de nome vazio
            return BadRequest(new { erro = ex.Message });
        }
    }

    // GET 
    [HttpGet]
    public async Task<ActionResult<List<PessoaResponseDto>>> Listar()
    {
        var pessoas = await _service.ListarPessoas();
        // Converte cada entidade para DTO, evitando expor ciclo Pessoa <-> Transacao
        return Ok(pessoas.Select(PessoaResponseDto.FromEntity).ToList());
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        try
        {
            await _service.DeletarPessoa(id);
            return NoContent(); // Retorna HTTP 204 
        }
        catch (KeyNotFoundException ex)
        {
            // retorna 404 se n existir 
            return NotFound(new { erro = ex.Message });
        }
    }
}

// Classe auxiliar para receber o JSON limpo do Postman e React
public class CriarPessoaRequest
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}