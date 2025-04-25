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
    public partial class FrmEliminarProducto : Form
    {
        public FrmEliminarProducto()
        {
            InitializeComponent();
        }


        // Objeto para manejar la conexión
        ClsConexionBD conexionBD = new ClsConexionBD();

        private void FrmEliminarProducto_Load(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Conectamos a la base de datos
            conexionBD.ConectarBD();

            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();

            if (codigo == "" || nombre == "")
            {
                MessageBox.Show("Por favor, completá el código y el nombre del producto.");
                return;
            }

            // Verificamos si existe
            string verificarQuery = "SELECT * FROM Productos WHERE Codigo = @codigo AND Nombre = @nombre";
            SqlCommand verificarCmd = new SqlCommand(verificarQuery, conexionBD.ObtenerConexion());
            verificarCmd.Parameters.AddWithValue("@codigo", codigo);
            verificarCmd.Parameters.AddWithValue("@nombre", nombre);

            SqlDataReader reader = verificarCmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close(); // Cerramos antes del DELETE

                string deleteQuery = "DELETE FROM Productos WHERE Codigo = @codigo AND Nombre = @nombre";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conexionBD.ObtenerConexion());
                deleteCmd.Parameters.AddWithValue("@codigo", codigo);
                deleteCmd.Parameters.AddWithValue("@nombre", nombre);
                deleteCmd.ExecuteNonQuery();

                MessageBox.Show("✅ Producto eliminado.");

                // Mostramos productos restantes con ese código o nombre
                string consultaRestantes = "SELECT Codigo, Nombre, Descripcion, Precio, Stock FROM Productos WHERE Codigo = @codigo OR Nombre = @nombre";

                SqlCommand restantesCmd = new SqlCommand(consultaRestantes, conexionBD.ObtenerConexion());
                restantesCmd.Parameters.AddWithValue("@codigo", codigo);
                restantesCmd.Parameters.AddWithValue("@nombre", nombre);

                SqlDataAdapter adaptador = new SqlDataAdapter(restantesCmd);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                dgvProductos.DataSource = tabla;
            }
            else
            {
                reader.Close();
                MessageBox.Show("Producto no encontrado. No se realizó ninguna eliminación.");
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
