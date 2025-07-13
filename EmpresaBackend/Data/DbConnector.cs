using System.Data;
using Microsoft.Data.SqlClient;


namespace EmpresaBackend.Data
{
    public class DbConnector
    {
        private readonly string _connectionString;

        public DbConnector(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public int ExecuteNonQuery(string procedureName, Action<SqlCommand> paramBuilder)
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand(procedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            paramBuilder?.Invoke(cmd);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public SqlDataReader ExecuteReader(string procedureName, Action<SqlCommand> paramBuilder)
        {
            var conn = GetConnection();
            var cmd = new SqlCommand(procedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            paramBuilder?.Invoke(cmd);
            conn.Open();

            // Reader must be closed externally
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
