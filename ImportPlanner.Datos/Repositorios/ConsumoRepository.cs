using ImportacionesPlanner.Datos.Conexion;
using ImportacionesPlanner.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ImportacionesPlanner.Datos.Repositorios
{
    public class ConsumoRepository
    {
        private const string QUERY_CONSUMO_VENTANA = @"
                            SELECT
                                a.Articulo,
                                ISNULL(v.C1,0) AS C1,
                                ISNULL(v.C2,0) AS C2,
                                ISNULL(v.C3,0) AS C3
                            FROM Bit_Vistas.dbo.Bit_Articulo a

                            LEFT JOIN
                            (
                                SELECT
                                    v.Articulo,

                                    SUM(
                                        CASE
                                            WHEN v.Fecha BETWEEN @Inicio1 AND @Fin1
                                            THEN v.Cantidad
                                            ELSE 0
                                        END
                                    ) AS C1,

                                    SUM(
                                        CASE
                                            WHEN v.Fecha BETWEEN @Inicio2 AND @Fin2
                                            THEN v.Cantidad
                                            ELSE 0
                                        END
                                    ) AS C2,

                                    SUM(
                                        CASE
                                            WHEN v.Fecha BETWEEN @Inicio3 AND @Fin3
                                            THEN v.Cantidad
                                            ELSE 0
                                        END
                                    ) AS C3

                                FROM Bit_Vistas.dbo.Bit_VentasHistorico v

                                WHERE v.Fecha >= @Inicio3

                                GROUP BY v.Articulo

                            ) v
                                ON a.Articulo = v.Articulo

                            WHERE
                                (@Proveedor = 'TODOS'
                                 OR a.ProvHabitual = @Proveedor)

                                AND (@Familia = 0
                                     OR a.Familia = @Familia)

                                AND ISNULL(a.param2,'') <> 'Bajas'

                            ORDER BY a.Articulo";

        public Dictionary<string, ConsumoVentana> ObtenerConsumoVentana(string proveedor, int familia, DateTime fechaInicioVentana, DateTime fechaFinVentana)
        {
            var resultado = new Dictionary<string, ConsumoVentana>();

            using (SqlConnection conn = new ConexionSqlServer().ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand(QUERY_CONSUMO_VENTANA, conn))
            {
                ConfigurarParametros(cmd, proveedor, familia, fechaInicioVentana, fechaFinVentana);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string articulo = reader["Articulo"].ToString();

                        decimal c1 = ObtenerDecimal(reader, "C1");

                        decimal c2 = ObtenerDecimal(reader, "C2");

                        decimal c3 = ObtenerDecimal(reader, "C3");

                        resultado[articulo] =new ConsumoVentana
                                                               {
                                                                   Anio1 = c1,
                                                                   Anio2 = c2,
                                                                   Anio3 = c3,
                                                                   Promedio = (c1 + c2 + c3) / 3m
                                                               };
                    }
                }
            }

            return resultado;
        }

        private void ConfigurarParametros(SqlCommand cmd, string proveedor, int familia, DateTime fechaInicioVentana, DateTime fechaFinVentana)
        {
            cmd.Parameters.AddWithValue("@Proveedor", proveedor);
            cmd.Parameters.AddWithValue("@Familia", familia);
            cmd.Parameters.AddWithValue("@Inicio1", fechaInicioVentana);
            cmd.Parameters.AddWithValue("@Fin1", fechaFinVentana);
            cmd.Parameters.AddWithValue("@Inicio2", fechaInicioVentana.AddYears(-1));
            cmd.Parameters.AddWithValue("@Fin2", fechaFinVentana.AddYears(-1));
            cmd.Parameters.AddWithValue("@Inicio3", fechaInicioVentana.AddYears(-2));
            cmd.Parameters.AddWithValue("@Fin3", fechaFinVentana.AddYears(-2));
        }

        private decimal ObtenerDecimal(SqlDataReader reader, string campo)
        {
            return reader[campo] == DBNull.Value ? 0 : Convert.ToDecimal(reader[campo]);
        }
    }
}