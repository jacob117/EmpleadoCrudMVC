using EmpresaFrontend.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CatalogoController : ControllerBase
{
    private readonly ICatalogoService _catalogo;

    public CatalogoController(ICatalogoService catalogo)
    {
        _catalogo = catalogo;
    }

    // GET: api/catalogo
    [HttpGet("{id}")]
    public IActionResult ObtenerCatalogo(int id)
    {
        var items = _catalogo.ObtenerCatalogo(id)?.ToList() ?? new List<Catalogo>();
        return Ok(items); 
    }
}
