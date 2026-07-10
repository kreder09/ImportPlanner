using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportacionesPlanner.Datos.Conexion;
using System.Data.SqlClient;

namespace ImportacionesPlanner.Datos.Repositorios
{
    public class ProveedorRepository
    {
        private const string QUERY_PROVEEDORES = @"
            SELECT
                p.Proveedor,
                MAX(p.RazonSocial) AS RazonSocial
            FROM Bit_Vistas.dbo.Bit_Proveedor p
            WHERE EXISTS
            (
                SELECT 1
                FROM Bit_Vistas.dbo.Bit_Articulo a
                WHERE a.ProvHabitual = p.Proveedor
            )
            GROUP BY p.Proveedor
            ORDER BY MAX(p.RazonSocial)";

        private const string QUERY_FAMILIAS = @"
            SELECT
                Familia,
                Descripcion
            FROM Bit_Vistas.dbo.Bit_Familia
            ORDER BY Familia";

        public Dictionary<string, string> ObtenerProveedores()
        {
            var resultado = new Dictionary<string, string>
            {
                ["TODOS"] = "Proveedor 1"
            };

            using (SqlConnection conn = new ConexionSqlServer().ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(QUERY_PROVEEDORES, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string codigo = reader["Proveedor"]?.ToString() ?? "";

                        string nombre = reader["RazonSocial"]?.ToString() ?? "";

                        if (!string.IsNullOrWhiteSpace(codigo))
                        {
                            resultado[codigo] = nombre;
                        }
                    }
                }
            }

            return resultado;
        }

        public Dictionary<int, string> ObtenerFamilias()
        {
            var resultado = new Dictionary<int, string>
            {
                [0] = "Familia 3"
            };

            using (SqlConnection conn = new ConexionSqlServer().ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(QUERY_FAMILIAS, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int familia = reader["Familia"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Familia"]);

                        string descripcion = reader["Descripcion"]?.ToString() ?? "";

                        if (!resultado.ContainsKey(familia))
                        {
                            resultado[familia] = $"{familia} - {descripcion}";
                        }
                    }
                }
            }

            return resultado;
        }
    }



}