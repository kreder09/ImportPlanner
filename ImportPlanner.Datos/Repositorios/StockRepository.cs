using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportacionesPlanner.Datos.Conexion;
using Oracle.ManagedDataAccess.Client;

namespace ImportacionesPlanner.Datos.Repositorios
{
        public class StockRepository
        {
            private const string QUERY_STOCK_DISPONIBLE = @"
SELECT
    stk.qt_stock - NVL(prep.qt_stock,0) disponible
FROM
(
    -- Stock físico disponible
    SELECT
        cd_produto,
        SUM(qt_estoque) qt_stock
    FROM t_stock
    WHERE cd_empresa = 1
      AND cd_produto = :Articulo
      AND dt_inventario IS NOT NULL
      AND cd_endereco NOT IN
      (
        '1FR00000',
        '1MA00000',
        '1GX00000'
      )
    GROUP BY cd_produto
) stk

LEFT JOIN
(
    -- Stock comprometido en ventas confirmadas
    SELECT
        dp.cd_produto,
        SUM(NVL(dp.qt_preparado,0)) qt_stock
    FROM t_contenedor co
    INNER JOIN t_det_picking dp
        ON co.nu_contenedor = dp.nu_contenedor
       AND co.nu_preparacion = dp.nu_preparacion
    WHERE co.cd_situacao IN (602)
      AND co.cd_camion_facturado IS NOT NULL
      AND dp.cd_empresa = 1
      AND dp.cd_produto = :Articulo
    GROUP BY dp.cd_produto
) prep
ON stk.cd_produto = prep.cd_produto";

            public decimal ObtenerStockDisponible(string articulo)
            {
                using (OracleConnection conn =
                       new ConexionOracle().ObtenerConexion())
                using (OracleCommand cmd =
                       new OracleCommand(
                           QUERY_STOCK_DISPONIBLE,
                           conn))
                {
                    cmd.Parameters.Add(
                        new OracleParameter(
                            "Articulo",
                            articulo));

                    conn.Open();

                    object result =
                        cmd.ExecuteScalar();

                    if (result == null ||
                        result == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(result);
                }
            }


        public Dictionary<string, decimal> ObtenerStocksDisponibles()
        {
            var resultado =
                new Dictionary<string, decimal>();

            string query = @"
        SELECT
            stk.cd_produto,
            stk.qt_stock - NVL(prep.qt_stock,0) disponible

        FROM
        (
            SELECT
                cd_produto,
                SUM(qt_estoque) qt_stock

            FROM t_stock

            WHERE cd_empresa = 1
              AND dt_inventario IS NOT NULL
              AND cd_endereco NOT IN
              (
                '1FR00000',
                '1MA00000',
                '1GX00000'
              )

            GROUP BY cd_produto

        ) stk

        LEFT JOIN
        (
            SELECT
                dp.cd_produto,
                SUM(NVL(dp.qt_preparado,0)) qt_stock

            FROM t_contenedor co

            INNER JOIN t_det_picking dp
                ON co.nu_contenedor = dp.nu_contenedor
               AND co.nu_preparacion = dp.nu_preparacion

            WHERE co.cd_situacao IN (602)
              AND co.cd_camion_facturado IS NOT NULL
              AND dp.cd_empresa = 1

            GROUP BY dp.cd_produto

        ) prep

        ON stk.cd_produto = prep.cd_produto";

            using (OracleConnection conn =
                   new ConexionOracle().ObtenerConexion())
            using (OracleCommand cmd =
                   new OracleCommand(query, conn))
            {
                conn.Open();

                using (OracleDataReader reader =
                       cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string articulo =
                            reader["cd_produto"].ToString();

                        decimal stock =
                            reader["disponible"] == DBNull.Value
                            ? 0
                            : Convert.ToDecimal(
                                reader["disponible"]);

                        resultado[articulo] = stock;
                    }
                }
            }

            return resultado;
        }



    }
    }