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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void agregarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAgregarProducto fp = new FrmAgregarProducto();
            fp.Show();
            this.Hide();
        }

        private void buscarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBuscarProducto fp = new FrmBuscarProducto();
            fp.Show();
            this.Hide();
        }

        private void modificarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmModificarProducto fp = new FrmModificarProducto();
            fp.Show();
            this.Hide();
        }

        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEliminarProducto fp = new FrmEliminarProducto();
            fp.Show();
            this.Hide();
        }

        private void reportesDeInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportesInventario fp = new FrmReportesInventario();
            fp.Show();
            this.Hide();
        }

        private void controlDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStockBajo fp = new FrmStockBajo();
            fp.Show();
            this.Hide();
        }
    }
}
