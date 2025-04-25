using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static pryRecioConexionBD.AGINP.FrmAgregarProducto;


namespace pryRecioConexionBD.AGINP
{
    public partial class FrmBuscarProducto : Form
    {
        public FrmBuscarProducto()
        {
            InitializeComponent();
        }


        ClsConexionBD conexionBD = new ClsConexionBD();

        private void FrmBuscarProducto_Load(object sender, EventArgs e)
        {
            conexionBD.ConectarBD();

            // Cargamos las categorías en el ComboBox
            string query = "SELECT Id, Nombre FROM Categorias";
            SqlCommand cmd = new SqlCommand(query, conexionBD.ObtenerConexion());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cboCategoria.Items.Add(new ComboboxItem
                {
                    Text = reader["Nombre"].ToString(),
                    Value = reader["Id"]
                });
            }

            reader.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conexionBD.ConectarBD();

            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            object categoriaId = cboCategoria.SelectedItem != null ? ((ComboboxItem)cboCategoria.SelectedItem).Value : null;

            string query = "SELECT P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, C.Nombre AS Categoria " +
                           "FROM Productos P INNER JOIN Categorias C ON P.CategoriaId = C.Id WHERE 1=1";

            // Armamos la consulta dinámica según los filtros
            if (codigo != "")
                query += " AND P.Codigo = @codigo";
            if (nombre != "")
                query += " AND P.Nombre LIKE @nombre";
            if (categoriaId != null)
                query += " AND P.CategoriaId = @categoriaId";

            SqlCommand cmd = new SqlCommand(query, conexionBD.ObtenerConexion());

            if (codigo != "")
                cmd.Parameters.AddWithValue("@codigo", codigo);
            if (nombre != "")
                cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
            if (categoriaId != null)
                cmd.Parameters.AddWithValue("@categoriaId", categoriaId);

            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            dgvResultados.DataSource = tabla;
        }

        // Clase auxiliar para el ComboBox
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void volverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMenu fp = new FrmMenu();
            fp.Show();
            this.Hide();
        }
    }
}
