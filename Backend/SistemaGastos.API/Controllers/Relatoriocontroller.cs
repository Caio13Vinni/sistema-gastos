using Microsoft.AspNetCore.Mvc;
using SistemaGastos.API.Dtos;
using SistemaGastos.API.Services;

namespace SistemaGastos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly RelatorioService _service;

    public RelatorioController(RelatorioService service)
    {
        _service = service;
    }


    [HttpGet("totais")]
    public async Task<ActionResult<RelatorioTotaisDto>> ObterTotais()
    {
        var relatorio = await _service.ObterTotais();
        return Ok(relatorio);
    }
}
