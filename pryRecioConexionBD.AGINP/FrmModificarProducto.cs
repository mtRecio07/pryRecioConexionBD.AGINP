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
    public partial class FrmModificarProducto : Form
    {
        public FrmModificarProducto()
        {
            InitializeComponent();
        }


        // Objeto de conexión a la base
        ClsConexionBD conexionBD = new ClsConexionBD();

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conexionBD.ConectarBD(); // Abrimos conexión

            string codigo = txtCodigo.Text.Trim();

            // Consulta para obtener los datos del producto
            string query = "SELECT Precio, Stock FROM Productos WHERE Codigo = @codigo";
            SqlCommand cmd = new SqlCommand(query, conexionBD.ObtenerConexion());
            cmd.Parameters.AddWithValue("@codigo", codigo);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txtPrecio.Text = reader["Precio"].ToString();
                txtStock.Text = reader["Stock"].ToString();
            }
            else
            {
                MessageBox.Show("Producto no encontrado.");
            }

            reader.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            conexionBD.ConectarBD();

            string codigo = txtCodigo.Text.Trim();

            try
            {
                decimal nuevoPrecio = Convert.ToDecimal(txtPrecio.Text);
                int nuevoStock = Convert.ToInt32(txtStock.Text);

                string updateQuery = "UPDATE Productos SET Precio = @precio, Stock = @stock WHERE Codigo = @codigo";
                SqlCommand cmd = new SqlCommand(updateQuery, conexionBD.ObtenerConexion());
                cmd.Parameters.AddWithValue("@precio", nuevoPrecio);
                cmd.Parameters.AddWithValue("@stock", nuevoStock);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.ExecuteNonQuery();

                MessageBox.Show("✅ Producto actualizado con éxito.");

                // Mostramos los datos actualizados en la grilla
                string selectQuery = "SELECT Codigo, Nombre, Descripcion, Precio, Stock FROM Productos WHERE Codigo = @codigo";
                SqlCommand selectCmd = new SqlCommand(selectQuery, conexionBD.ObtenerConexion());
                selectCmd.Parameters.AddWithValue("@codigo", codigo);

                SqlDataAdapter adaptador = new SqlDataAdapter(selectCmd);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                dgvModificados.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al actualizar: " + ex.Message);
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
