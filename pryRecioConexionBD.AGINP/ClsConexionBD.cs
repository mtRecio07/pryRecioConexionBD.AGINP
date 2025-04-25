using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryRecioConexionBD.AGINP
{
    internal class ClsConexionBD
    {
        // Cadena de conexión a SQL Server
        private string cadenaConexion = "Server=localhost;Database=Comercio;Trusted_Connection=True;";

        // Conexión SQL
        private SqlConnection conexionBaseDatos;

        // Propiedad pública para acceder a la conexión
        public SqlConnection conexion
        {
            get { return conexionBaseDatos; }
        }

        // Nombre de la base de datos (opcional)
        public string nombreBaseDeDatos;

        // Método para abrir la conexión
        public void ConectarBD()
        {
            try
            {
                conexionBaseDatos = new SqlConnection(cadenaConexion);
                conexionBaseDatos.Open();
                nombreBaseDeDatos = conexionBaseDatos.Database;

                MessageBox.Show("Conexión exitosa a la base de datos: " + nombreBaseDeDatos);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al conectar: " + error.Message);
            }
        }

        // Método alternativo para obtener la conexión
        public SqlConnection ObtenerConexion()
        {
            return conexionBaseDatos;
        }
    }
}
