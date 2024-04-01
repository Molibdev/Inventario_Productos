using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario2024.Forms
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        //Redirecciones
        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionInventario frmGI = new frmGestionInventario();
            frmGI.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionClientes frmGC = new frmGestionClientes();
            frmGC.Show();
        }

        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroProductos frmRP = new frmRegistroProductos();
            frmRP.Show();   
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroVentas frmRV = new frmRegistroVentas();
            frmRV.Show();
        }
    }
}
