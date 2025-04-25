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

namespace pryRecioConexionBD.AGINP
{
    public partial class FrmStockBajo : Form
    {
        public FrmStockBajo()
        {
            InitializeComponent();
        }


        ClsConexionBD conexionBD = new ClsConexionBD();

        private void FrmStockBajo_Load(object sender, EventArgs e)
        {
            conexionBD.ConectarBD();

            // Cargamos las categorías en el ComboBox (con el código que venís usando)
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

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategoria.SelectedItem == null) return;

            ComboboxItem itemSeleccionado = (ComboboxItem)cboCategoria.SelectedItem;

            string query = "SELECT Codigo, Nombre, Stock FROM Productos WHERE Stock <= 5 AND CategoriaId = @categoriaId";
            SqlCommand cmd = new SqlCommand(query, conexionBD.ObtenerConexion());
            cmd.Parameters.AddWithValue("@categoriaId", itemSeleccionado.Value);

            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            dgvAlertas.DataSource = tabla;
        }


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
