using EmpresaBackend.Data;
using EmpresaBackend.Models;
using System.Data.SqlClient;

namespace EmpresaBackend.Services
{
    public class EmpleadoService
    {
        private readonly DbConnector _db;

        public EmpleadoService(DbConnector db)
        {
            _db = db;
        }

        public List<Empleado> ObtenerJerarquia()
        {
            List<Empleado> empleados = new();

            using var reader = _db.ExecuteReader("sp_Empleado", cmd =>
            {
                cmd.Parameters.AddWithValue("@Tipo", 1); 
                cmd.Parameters.AddWithValue("@Codigo", null); 
            });
            while (reader.Read())
            {
                empleados.Add(new Empleado
                {
                    Codigo = (int)reader["Codigo"],
                    Puesto = reader["Puesto"].ToString(),
                    CodigoPuesto = (int)reader["codigoPuesto"],
                    Nombre = reader["Nombre"].ToString(),
                    CodigoJefe = reader["CodigoJefe"] == DBNull.Value ? null : (int?)reader["CodigoJefe"]
                });
            }

            return empleados;
        }
        
         public List<Catalogo> ObtenerCatalgo(int id)
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

        public List<Empleado> ObtenerEmpleado(int id)
        {
            List<Empleado> empleados = new();

            using var reader = _db.ExecuteReader("sp_Empleado", cmd =>
            {
                cmd.Parameters.AddWithValue("@Tipo", 2);
                cmd.Parameters.AddWithValue("@Codigo", id);
            });
            while (reader.Read())
            {
                empleados.Add(new Empleado
                {
                    Codigo = (int)reader["Codigo"],
                    CodigoPuesto = (int)reader["codigoPuesto"],
                    Nombre = reader["Nombre"].ToString(),
                    CodigoJefe = reader["CodigoJefe"] == DBNull.Value ? null : (int?)reader["CodigoJefe"]
                });
            }

            return empleados;
        }

        public void Insertar(Empleado e)
        {
            _db.ExecuteNonQuery("sp_InsertEmpleado", cmd =>
            {
                cmd.Parameters.AddWithValue("@codigoPuesto", e.CodigoPuesto);
                cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
                cmd.Parameters.AddWithValue("@CodigoJefe", (object?)e.CodigoJefe ?? DBNull.Value);
            });
        }

        public void Actualizar(Empleado e)
        {
            _db.ExecuteNonQuery("sp_UpdateEmpleado", cmd =>
            {
                cmd.Parameters.AddWithValue("@codigoPuesto", e.CodigoPuesto);
                cmd.Parameters.AddWithValue("@codigo", e.Codigo);
                cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
                cmd.Parameters.AddWithValue("@CodigoJefe", (object?)e.CodigoJefe ?? DBNull.Value);
            });
        }

        public void Eliminar(int codigo)
        {
            _db.ExecuteNonQuery("sp_DeleteEmpleado", cmd =>
            {
                cmd.Parameters.AddWithValue("@Codigo", codigo);
            });
        }
    }
}
