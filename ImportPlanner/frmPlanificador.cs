using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImportacionesPlanner.Datos.Conexion;
using System.Data.SqlClient;
using ImportacionesPlanner.Datos.Repositorios;
using ImportacionesPlanner.Datos;
using ImportacionesPlanner.Datos.Modelos;

namespace ImportacionesPlanner
{
    public partial class frmPlanificador : Form
    {
        public frmPlanificador()
        {
            InitializeComponent();
        }

        private void frmPlanificador_Load(object sender, EventArgs e)
        {
            try
            {
                InicializarControles();
                ConfigurarGrid();
                CargarCombos();
                VerificarConexiones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando formulario: " + ex.Message);
            }
        }

        private void InicializarControles()
        {
            cmbBaseCalculo.Items.Clear();

            cmbBaseCalculo.Items.Add("Año pasado");
            cmbBaseCalculo.Items.Add("Promedio 3 años");
            cmbBaseCalculo.Items.Add("Año pasado + % crecimiento");

            cmbBaseCalculo.SelectedIndex = 0;

            nudCrecimiento.Enabled = false;
        }

        private void ConfigurarGrid()
        {
            dgvResultados.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgvResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvResultados.RowTemplate.Height = 28;
            dgvResultados.EnableHeadersVisualStyles = false;
            dgvResultados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 60, 90);
            dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarCombos()
        {
            var proveedorRepo = new ProveedorRepository();
            CargarProveedores(proveedorRepo);
            CargarFamilias(proveedorRepo);
        }

        private void CargarProveedores(ProveedorRepository proveedorRepo)
        {
            var proveedores = proveedorRepo.ObtenerProveedores();

            cmbProveedor.DataSource = null;
            cmbProveedor.DataSource = new BindingSource(proveedores, null);
            cmbProveedor.DisplayMember = "Value";
            cmbProveedor.ValueMember = "Key";
        }

        private void CargarFamilias(ProveedorRepository proveedorRepo)
        {
            var familias = proveedorRepo.ObtenerFamilias();

            cmbFamilia.DataSource = null;
            cmbFamilia.DataSource = new BindingSource(familias, null);
            cmbFamilia.DisplayMember = "Value";
            cmbFamilia.ValueMember = "Key";
        }

        private void VerificarConexiones()
        {
            VerificarConexionBit();
            VerificarConexionWis();
        }

        private void VerificarConexionBit()
        {
            try
            {
                var conexion = new ConexionSqlServer();

                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    conn.Open();
                    MessageBox.Show("Conexión ERP exitosa");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar ERP: "+ ex.Message);
            }
        }

        private void VerificarConexionWis()
        {
            try
            {
                var conexionOracle = new ConexionOracle();

                if (conexionOracle.ProbarConexion())
                {
                    MessageBox.Show("Conexión WMS exitosa");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Error al conectar WMS: " + ex.Message);
            }
        }

        private ParametrosPlanificacion ObtenerParametros()
        {
            return new ParametrosPlanificacion
            {
                Proveedor = cmbProveedor.SelectedValue.ToString(),

                Familia = Convert.ToInt32(cmbFamilia.SelectedValue),

                DiasLead =(int)nudLeadTime.Value,

                DiasFrecuencia =(int)nudFrecuencia.Value,

                BaseCalculo = cmbBaseCalculo.SelectedIndex,

                Crecimiento = nudCrecimiento.Value
            };
        }

        private bool ValidarParametros()
        {
            if (cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return false;
            }

            int frecuenciaMeses = (int)Math.Ceiling(nudFrecuencia.Value / 30m);

            if (frecuenciaMeses <= 0)
            {
                MessageBox.Show("La frecuencia debe ser mayor a 0.");
                return false;
            }

            return true;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            btnGenerar.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                if (!ValidarParametros())
                    return;

                var parametros = ObtenerParametros();

                GenerarPlanificacion(parametros);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar cálculo: " + ex.Message);
            }
            finally
            {
                btnGenerar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void GenerarPlanificacion(ParametrosPlanificacion parametros)
        {
            string proveedor = parametros.Proveedor;

            int familia = parametros.Familia;

            int diasLead = parametros.DiasLead;

            int diasFrecuencia = parametros.DiasFrecuencia;

            int frecuenciaMeses = (int)Math.Ceiling(diasFrecuencia / 30m);

            int diasVentana = diasLead + diasFrecuencia;

            DateTime hoy = DateTime.Today;

            DateTime fechaLlegada = hoy.AddDays(diasLead);

            DateTime inicioVentana = hoy.AddYears(-1);

            DateTime finVentana = inicioVentana.AddDays(diasVentana);

            string textoHistorico = "Histórico: " + inicioVentana.ToString("dd/MM/yyyy") + " - " + finVentana.ToString("dd/MM/yyyy");

            string detalleBase = "";

            switch (parametros.BaseCalculo)
            {
                case 0:
                    detalleBase = "(Consumo del año pasado en este período)";
                    break;

                case 1:
                    detalleBase = "(Promedio de este período en los últimos 3 años)";
                    break;

                case 2:
                    detalleBase = "(Año pasado ajustado por crecimiento)";
                    break;
            }

            lblResumen.Text =
                "Llega estimado: "
                + fechaLlegada.ToString("dd/MM/yyyy")
                + Environment.NewLine
                + textoHistorico
                + Environment.NewLine
                + detalleBase;

            var repoConsumo = new ConsumoRepository();

            var repoStock = new StockRepository();

            var stocks = repoStock.ObtenerStocksDisponibles();

            var repoImport = new ImportacionesRepository();

            var consumos = repoConsumo.ObtenerConsumoVentana(proveedor, familia, inicioVentana, finVentana);

            var enCamino = repoImport.ObtenerEnCamino(proveedor);

            ConfigurarColumnasResultados(inicioVentana, finVentana);

            CargarResultados(consumos, enCamino, stocks, frecuenciaMeses, parametros);

            ActualizarImportaciones(repoImport, proveedor);
        }

        private void CargarResultados(Dictionary<string, ConsumoVentana> consumos, Dictionary<string, decimal> enCamino, Dictionary<string, decimal> stocks, int frecuenciaMeses, ParametrosPlanificacion parametros)
        {
            foreach (var item in consumos.OrderBy(x => x.Key))
            {
                string articulo = item.Key;

                decimal consumoAnio1 = item.Value.Anio1;

                decimal consumoAnio2 = item.Value.Anio2;

                decimal consumoAnio3 = item.Value.Anio3;

                decimal promedio = item.Value.Promedio;

                decimal stock = stocks.ContainsKey(articulo) ? stocks[articulo] : 0;

                decimal baseConsumo;

                switch (parametros.BaseCalculo)
                {
                    case 0:
                        baseConsumo = consumoAnio1;
                        break;

                    case 1:
                        baseConsumo = promedio;
                        break;

                    case 2:
                        decimal crecimiento = parametros.Crecimiento / 100m;

                        baseConsumo = consumoAnio1 * (1 + crecimiento);
                        break;

                    default:
                        baseConsumo = promedio;
                        break;
                }

                decimal factorCrecimientoDefault = 1.5m;

                baseConsumo =  baseConsumo * factorCrecimientoDefault;

                decimal enCaminoArticulo = enCamino.ContainsKey(articulo) ? enCamino[articulo] : 0;

                decimal sugerencia = baseConsumo - stock - enCaminoArticulo;

                if (sugerencia < 0)
                    sugerencia = 0;

                sugerencia = Math.Ceiling(sugerencia);

                decimal consumoMensual = 0;

                if (frecuenciaMeses > 0)
                {
                    consumoMensual = baseConsumo / frecuenciaMeses;
                }

                decimal cobertura;

                if (consumoMensual > 0)
                {
                    cobertura = stock / consumoMensual;
                }
                else
                {
                    if (stock > 0)
                        cobertura = -1;
                    else
                        cobertura = 0;
                }

                cobertura =
                    cobertura > 0 ? Math.Round(cobertura, 2) : cobertura;

                object coberturaMostrar = cobertura == -1 ? "∞" : (object)cobertura;

                int rowIndex = dgvResultados.Rows.Add(articulo, consumoAnio3, consumoAnio2, consumoAnio1, promedio, stock, coberturaMostrar, sugerencia, enCaminoArticulo);

                AplicarEstilosFila(rowIndex, cobertura, enCaminoArticulo);
            }
        }

        private void ConfigurarColumnasResultados(DateTime inicioVentana, DateTime finVentana)
        {
            dgvResultados.Rows.Clear();
            dgvResultados.Columns.Clear();

            dgvResultados.Columns.Add("Articulo", "Artículo");
            dgvResultados.Columns.Add("Anio3", "");
            dgvResultados.Columns.Add("Anio2", "");
            dgvResultados.Columns.Add("Anio1", "");
            dgvResultados.Columns.Add("Promedio", "Promedio");
            dgvResultados.Columns.Add("Stock", "Stock");
            dgvResultados.Columns.Add("Cobertura", "Cobertura (meses)");
            dgvResultados.Columns.Add("Sugerencia", "Sugerido");
            dgvResultados.Columns.Add("EnCamino", "En Camino");

            int anio1 = inicioVentana.Year;
            int anio2 = anio1 - 1;
            int anio3 = anio1 - 2;

            string rango = inicioVentana.ToString("MMM") + " - " + finVentana.ToString("MMM");

            dgvResultados.Columns["Anio1"].HeaderText = $"{anio1}\n{rango}";

            dgvResultados.Columns["Anio2"].HeaderText = $"{anio2}\n{rango}";

            dgvResultados.Columns["Anio3"].HeaderText = $"{anio3}\n{rango}";

            dgvResultados.EnableHeadersVisualStyles = false;

            dgvResultados.BorderStyle = BorderStyle.None;

            dgvResultados.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgvResultados.RowHeadersVisible = false;

            dgvResultados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 70, 95);

            dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvResultados.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResultados.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvResultados.ColumnHeadersHeight = 65;

            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvResultados.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvResultados.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 240);

            dgvResultados.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvResultados.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgvResultados.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResultados.Columns["Promedio"].DefaultCellStyle.Format = "N2";

            dgvResultados.Columns["Articulo"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void AplicarEstilosFila(int rowIndex, decimal cobertura, decimal enCaminoArticulo)
        {
            DataGridViewRow row = dgvResultados.Rows[rowIndex];

            if (cobertura == -1)
            {
                row.Cells["Cobertura"].Style.BackColor = Color.FromArgb(235, 235, 235);
            }
            else if (cobertura < 1)
            {
                row.Cells["Cobertura"].Style.BackColor = Color.FromArgb(255, 230, 230);
            }
            else if (cobertura < 3)
            {
                row.Cells["Cobertura"].Style.BackColor = Color.FromArgb(255, 250, 200);
            }
            else
            {
                row.Cells["Cobertura"].Style.BackColor = Color.FromArgb(230, 255, 230);
            }

            if (enCaminoArticulo > 0)
            {
                row.Cells["EnCamino"].Style.BackColor = Color.LightSkyBlue;
            }
        }

        private void ActualizarImportaciones(ImportacionesRepository repoImport, string proveedor)
        {
            var carpetas = repoImport.ObtenerCarpetasEnCamino(proveedor);

            if (carpetas.Count == 0)
            {
                lblImportaciones.Text = "Importaciones en camino: Ninguna";
            }
            else
            {
                lblImportaciones.Text = "Importaciones en camino: " + string.Join(", ", carpetas);
            }
        }

        private void cmbBaseCalculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudCrecimiento.Enabled = cmbBaseCalculo.SelectedIndex == 2;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            foreach (DataGridViewRow row in dgvResultados.Rows)
            {
                if (row.IsNewRow) continue;

                string articulo = row.Cells["Articulo"].Value?.ToString().ToLower() ?? "";

                row.Visible = articulo.Contains(filtro);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
        }
    }
}
