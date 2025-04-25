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
    public partial class FrmAgregarProducto : Form
    {
        public FrmAgregarProducto()
        {
            InitializeComponent();
        }


        ClsConexionBD conexionBD = new ClsConexionBD();


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmFrmAgregarProducto_Load(object sender, EventArgs e)
        {
            conexionBD.ConectarBD();

            // Cargamos categorías
            string queryCategorias = "SELECT Id, Nombre FROM Categorias";
            SqlCommand cmdCat = new SqlCommand(queryCategorias, conexionBD.ObtenerConexion());
            SqlDataReader reader = cmdCat.ExecuteReader();

            while (reader.Read())
            {
                cboCategoria.Items.Add(new ComboboxItem
                {
                    Text = reader["Nombre"].ToString(),
                    Value = reader["Id"]
                });
            }

            reader.Close();

            // Cargar productos en la grilla
            CargarProductos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "" || txtNombre.Text == "" || txtPrecio.Text == "" || txtStock.Text == "" || cboCategoria.SelectedItem == null)
            {
                MessageBox.Show("⚠️ Por favor, completá todos los campos.");
                return;
            }

            try
            {
                string insertQuery = "INSERT INTO Productos (Codigo, Nombre, Descripcion, Precio, Stock, CategoriaId) " +
                                     "VALUES (@codigo, @nombre, @descripcion, @precio, @stock, @categoriaId)";

                SqlCommand cmd = new SqlCommand(insertQuery, conexionBD.ObtenerConexion());
                cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text));
                cmd.Parameters.AddWithValue("@categoriaId", ((ComboboxItem)cboCategoria.SelectedItem).Value);

                cmd.ExecuteNonQuery();

                MessageBox.Show("✅ Producto agregado con éxito.");

                // Limpiar campos
                txtCodigo.Clear();
                txtNombre.Clear();
                txtDescripcion.Clear();
                txtPrecio.Clear();
                txtStock.Clear();
                cboCategoria.SelectedIndex = -1;

                // Recargar la grilla
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al agregar: " + ex.Message);
            }
        }




        private void CargarProductos()
        {
            try
            {
                string query = "SELECT P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, C.Nombre AS Categoria " +
                               "FROM Productos P INNER JOIN Categorias C ON P.CategoriaId = C.Id";

                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexionBD.ObtenerConexion());
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                dgvProductos.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar productos: " + ex.Message);
            }
        }


        // Clase para ComboBox
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
