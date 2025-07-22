using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Route("api/tipos")]
[ApiController]

public class TipoController : ControllerBase
{
    private readonly ITipoService _tipoService;

    public TipoController(ITipoService tipoService)
    {
        _tipoService = tipoService;
    }

    [HttpPost]
    public async Task<ActionResult<TipoDTO>> PostTipo(TipoDTO tipoDTO)
    {
        var tipoDTOResult = await _tipoService.Add(tipoDTO);
        return Ok(tipoDTOResult);
    }


}