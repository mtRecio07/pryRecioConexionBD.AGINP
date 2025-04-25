using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryRecioConexionBD.AGINP
{
    public partial class FrmInicio : Form
    {
        public FrmInicio()
        {
            InitializeComponent();
        }


        private const string usuarioCorrecto = "admin";
        private const string contraseñaCorrecta = "1234";


        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text;

            if (usuario != usuarioCorrecto)
            {
                MessageBox.Show("Usuario incorrecto", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (contraseña != contraseñaCorrecta)
            {
                MessageBox.Show("Contraseña incorrecta", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Clear();
                txtContraseña.Focus();
                return;
            }

            // Si usuario y contraseña son correctos
            FrmMenu frmMenu = new FrmMenu();
            frmMenu.Show();
            this.Hide();
        }
    }
}
