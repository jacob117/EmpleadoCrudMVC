using EmpresaBackend.Data;
using EmpresaBackend.Models;
using System.Data.SqlClient;

namespace EmpresaBackend.Services
{
    public class CatalogoService
    {
        private readonly DbConnector _db;

        public CatalogoService(DbConnector db)
        {
            _db = db;
        }


        public List<Catalogo> ObtenerCatalogo(int id)
        {
            List<Catalogo> empleados = new();

            using var reader = _db.ExecuteReader("sp_Catalogos", cmd =>
            {
                cmd.Parameters.AddWithValue("@TipoCatalgo", id);
            });
            while (reader.Read())
            {
                empleados.Add(new Catalogo
                {
                    Codigo = (int)reader["Codigo"],
                    Nombre = reader["Nombre"].ToString(),
                });
            }

            return empleados;
        }

    }
}
