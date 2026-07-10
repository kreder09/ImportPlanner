using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace ImportacionesPlanner.Datos.Conexion
{
    public class ConexionOracle
    {
        private readonly string _connectionString = ConfigurationManager
                                .ConnectionStrings["WMS"]
                                .ConnectionString;

        public OracleConnection ObtenerConexion()
        {
            return new OracleConnection(_connectionString);
        }

        public bool ProbarConexion()
        {
            using (var conn = ObtenerConexion())
            {
                conn.Open();
                return conn.State == System.Data.ConnectionState.Open;
            }
        }
    }
}

