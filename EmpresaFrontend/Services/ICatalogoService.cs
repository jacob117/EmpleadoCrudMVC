using EmpresaFrontend.Models;

public interface ICatalogoService
{
    IEnumerable<Catalogo> ObtenerCatalogo(int id);
}
