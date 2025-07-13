namespace EmpresaFrontend.Models
{
    public class Empleado
    {
        public int Codigo { get; set; }
        public string Puesto { get; set; } = string.Empty;
        public int CodigoPuesto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int? CodigoJefe { get; set; }
        public string? NombreJefe { get; set; }

        public List<Empleado> Subordinados { get; set; } = new();
    }
}
