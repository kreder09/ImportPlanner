using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImportacionesPlanner.Datos.Conexion;
using System.Data.SqlClient;

namespace ImportacionesPlanner.Datos.Repositorios
{
    public class ImportacionesRepository
    {
        private const string FILTRO_IMPORTACIONES_EN_CAMINO = @"
            t.trx_type IN (1106,1130)
            AND t.vend_code = @Proveedor
            AND t.date_entered >= DATEADD(day,-200,GETDATE())

            AND NOT EXISTS
            (
                SELECT 1
                FROM BitData_Gianni.dbo.apchgtrx x WITH (NOLOCK)
                WHERE x.comments = t.comments
                AND x.trx_type IN (11124,11128)
            )

            AND NOT EXISTS
            (
                SELECT 1
                FROM BitData_Gianni.dbo.apchgtrx x WITH (NOLOCK)
                WHERE x.comments = t.comments
                AND x.trx_type IN (11120,11121)
            )";

        private const string QUERY_CARPETAS_EN_CAMINO = @"
            SELECT
                t.comments
            FROM BitData_Gianni.dbo.apchgtrx t WITH (NOLOCK)
            WHERE " + FILTRO_IMPORTACIONES_EN_CAMINO + @"
            GROUP BY t.comments
            ORDER BY t.comments";

        private const string QUERY_ARTICULOS_EN_CAMINO = @"
            SELECT
                d.item_code,
                SUM(d.qty_ordered) AS en_camino

            FROM BitData_Gianni.dbo.apchgtrx t WITH (NOLOCK)

            INNER JOIN BitData_Gianni.dbo.apchgdet d WITH (NOLOCK)
                ON t.trx_num = d.trx_num

            WHERE " + FILTRO_IMPORTACIONES_EN_CAMINO + @"

            GROUP BY d.item_code";

        public List<string> ObtenerCarpetasEnCamino(string proveedor)
        {
            List<string> carpetas = new List<string>();

            using (SqlConnection conn = new ConexionSqlServer().ObtenerConexion())
            {
                conn.Open();

                using (SqlCommand cmd = CrearComandoProveedor(QUERY_CARPETAS_EN_CAMINO, conn, proveedor))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carpetas.Add(reader["comments"].ToString());
                    }
                }
            }

            return carpetas;
        }

        public Dictionary<string, decimal> ObtenerEnCamino(string proveedor)
        {
            Dictionary<string, decimal> resultado = new Dictionary<string, decimal>();

            using (SqlConnection conn = new ConexionSqlServer().ObtenerConexion())
            {
                conn.Open();

                using (SqlCommand cmd = CrearComandoProveedor(QUERY_ARTICULOS_EN_CAMINO, conn, proveedor))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string articulo = reader["item_code"].ToString();

                        decimal cantidad = reader["en_camino"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["en_camino"]);

                        resultado[articulo] = cantidad;
                    }
                }
            }

            return resultado;
        }

        private SqlCommand CrearComandoProveedor(string query, SqlConnection conn, string proveedor)
        {
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Proveedor", proveedor);

            return cmd;
        }
    }
}
