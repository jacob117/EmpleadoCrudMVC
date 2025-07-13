using Microsoft.AspNetCore.Mvc;
using EmpresaBackend.Models;
using EmpresaBackend.Services;

namespace EmpresaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly CatalogoService _service;

        public CatalogoController(CatalogoService service)
        {
            _service = service;
        }

        [HttpGet]

        [HttpGet("{id}")]
        public IActionResult GetSelect(int id)
        {
            try
            {
                var resultado = _service.ObtenerCatalogo(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Loguear el error o devolver un mensaje
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

    }
}
