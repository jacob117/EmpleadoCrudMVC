using EmpresaFrontend.Models;

public interface IEmpleadoService
{
    IEnumerable<Empleado> ObtenerTodos();
    Empleado ObtenerPorId(int id);
    void Crear(Empleado emp);
    void Actualizar(Empleado emp);
    void Eliminar(int id);
}
