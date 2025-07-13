using Microsoft.AspNetCore.Mvc;
using EmpresaBackend.Models;
using EmpresaBackend.Services;

namespace EmpresaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoService _service;

        public EmpleadosController(EmpleadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var resultado = _service.ObtenerJerarquia();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Loguear el error o devolver un mensaje
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetSelect(int id)
        {
            try
            {
                var resultado = _service.ObtenerEmpleado(id).FirstOrDefault();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Loguear el error o devolver un mensaje
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Empleado empleado)
        {
            _service.Insertar(empleado);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Empleado empleado)
        {
            empleado.Codigo = id;
            _service.Actualizar(empleado);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Eliminar(id);
            return Ok();
        }
    }
}
