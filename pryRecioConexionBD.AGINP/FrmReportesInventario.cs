using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Data.SqlClient;

namespace pryRecioConexionBD.AGINP
{
    public partial class FrmReportesInventario : Form
    {
        public FrmReportesInventario()
        {
            InitializeComponent();
        }


        ClsConexionBD conexionBD = new ClsConexionBD();

        private void volverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMenu fp = new FrmMenu();
            fp.Show();
            this.Hide();
        }

        private void FrmReportesInventario_Load(object sender, EventArgs e)
        {
            conexionBD.ConectarBD();

            string query = "SELECT P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, C.Nombre AS Categoria " +
                           "FROM Productos P INNER JOIN Categorias C ON P.CategoriaId = C.Id";

            SqlDataAdapter adaptador = new SqlDataAdapter(query, conexionBD.ObtenerConexion());
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            dgvReporte.DataSource = tabla;
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (XLWorkbook libro = new XLWorkbook())
                        {
                            DataTable tabla = (DataTable)dgvReporte.DataSource;
                            libro.Worksheets.Add(tabla, "Inventario");
                            libro.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("✅ Reporte exportado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al exportar: " + ex.Message);
            }
        }
    }
}
