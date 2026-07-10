using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ImportacionesPlanner.Datos.Conexion
{
    public class ConexionSqlServer
    {
        private readonly string _connectionString = ConfigurationManager
                                .ConnectionStrings["ERP"]
                                .ConnectionString;

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
