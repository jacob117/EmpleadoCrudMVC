using EmpresaFrontend.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class EmpleadosController : Controller
{
    private readonly IEmpleadoService _empleadoService;

    public EmpleadosController(IEmpleadoService empleadoService)
    {
        _empleadoService = empleadoService;
    }

    // Vista jerárquica
    [HttpGet("/Empleados/Index")]
    public IActionResult Index()
    {
        var empleados = _empleadoService.ObtenerTodos()?.ToList() ?? new List<Empleado>();

        foreach (var emp in empleados)
            emp.Subordinados ??= new List<Empleado>();

        var jerarquia = ConstruirArbol(empleados);
        return View(jerarquia);
    }

    private List<Empleado> ConstruirArbol(List<Empleado> empleados)
    {
        var dic = empleados.ToDictionary(e => e.Codigo, e => e);
        var raiz = new List<Empleado>();

        foreach (var emp in empleados)
        {
            if (emp.CodigoJefe.HasValue && dic.ContainsKey(emp.CodigoJefe.Value))
                dic[emp.CodigoJefe.Value].Subordinados.Add(emp);
            else
                raiz.Add(emp);
        }

        return raiz;
    }

    // Vista de búsqueda con tabla y modal
    [HttpGet("/Empleados/Buscar")]
    public IActionResult Buscar()
    {
        return View();
    }

    // API: Get all
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? search = null, int page = 1, int pageSize = 20, string? puesto = null, int? jefeId = null)
    {
        var empleados = _empleadoService.ObtenerTodos();

        if (!string.IsNullOrWhiteSpace(search))
        {
            empleados = empleados.Where(e =>
                e.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                e.Codigo.ToString().Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(puesto))
            empleados = empleados.Where(e => e.Puesto == puesto);

        if (jefeId.HasValue)
            empleados = empleados.Where(e => e.CodigoJefe == jefeId.Value);

        foreach (var emp in empleados)
            emp.NombreJefe = empleados.FirstOrDefault(j => j.Codigo == emp.CodigoJefe)?.Nombre;

        var total = empleados.Count();
        var paged = empleados.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return Ok(new { data = paged, total, page, pageSize });
    }

    // API: Get by ID
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
       var emp = _empleadoService.ObtenerPorId(id);
    if (emp == null) return NotFound();
    return Ok(emp);
    }

    // API: Crear
    [HttpPost]
    public IActionResult Create([FromBody] Empleado emp)
    {
        _empleadoService.Crear(emp);
        return Ok();
    }

    // API: Actualizar
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Empleado emp)
    {
        if (id != emp.Codigo) return BadRequest();
        _empleadoService.Actualizar(emp);
        return Ok();
    }

    // API: Eliminar
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _empleadoService.Eliminar(id);
        return Ok();
    }
}
