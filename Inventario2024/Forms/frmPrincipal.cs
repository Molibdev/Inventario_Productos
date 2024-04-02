using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventario2024.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Inventario2024.Forms
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        //Redirecciones a otros formularios
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


        //Generación de informes *********

        //Generacion informe de ventas
        private void btnGenInfVentas_Click(object sender, EventArgs e)
        {
            //Crear archivo de excel
            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("Inventario_Ventas");

            //Obtener los datos de la tabla
            using (Inventario2024Entities iE = new Inventario2024Entities())
            {
                var consulta = (from venta in iE.Ventas
                                select venta).ToList();

                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("ID_Cliente");
                headerRow.CreateCell(1).SetCellValue("Fecha");
                headerRow.CreateCell(2).SetCellValue("ID_Producto");
                headerRow.CreateCell(3).SetCellValue("Cantidad");
                headerRow.CreateCell(4).SetCellValue("ID_Cliente");

                //Escribir los datos en el archivo excel
                int rowIndex = 1;
                
                foreach (var dato in consulta)
                {
                    IRow row = sheet.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(dato.ID_Cliente.Value);
                    row.CreateCell(1).SetCellValue(dato.Fecha.Value);
                    row.CreateCell(2).SetCellValue(dato.ID_Producto.Value);
                    row.CreateCell(3).SetCellValue(dato.Cantidad.Value);
                    row.CreateCell(4).SetCellValue(dato.ID_Cliente.Value);

                    rowIndex++;
                }

                //Guardar el archivo de excel
                using (var fileStream = new FileStream("Inventario_Ventas.xlsx", FileMode.Create))
                {
                    wb.Write(fileStream);
                }

                MessageBox.Show("Se ha creado el informe de ventas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        //Generación informe de Productos
        private void btnGenInfProd_Click(object sender, EventArgs e)
        {
            //Crear archivo de excel
            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("Inventario_Productos");

            //Obtener los datos de la tabla
            using (Inventario2024Entities iE = new Inventario2024Entities())
            {
                var consulta = (from producto in iE.Productos
                                select producto).ToList();

                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("ID_Producto");
                headerRow.CreateCell(1).SetCellValue("Nombre");
                headerRow.CreateCell(2).SetCellValue("Descripcion");
                headerRow.CreateCell(3).SetCellValue("Precio");
                headerRow.CreateCell(4).SetCellValue("Cantidad_Stock");
                headerRow.CreateCell(5).SetCellValue("ID_Categoria");

                //Escribir los datos en el archivo excel
                int rowIndex = 1;

                foreach (var dato in consulta)
                {
                    IRow row = sheet.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(dato.ID_Producto);
                    row.CreateCell(1).SetCellValue(dato.Nombre);
                    row.CreateCell(2).SetCellValue(dato.Descripcion);
                    row.CreateCell(3).SetCellValue(dato.Precio.ToString());
                    row.CreateCell(4).SetCellValue(dato.Cantidad_Stock.Value);
                    row.CreateCell(5).SetCellValue(dato.ID_Categoria.ToString());

                    rowIndex++;
                }

                //Guardar el archivo de excel
                using (var fileStream = new FileStream("Inventario_Productos.xlsx", FileMode.Create))
                {
                    wb.Write(fileStream);
                }

                MessageBox.Show("Se ha creado el informe de Productos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }


        //Generación informe de Clientes
        private void btnGenInfCli_Click(object sender, EventArgs e)
        {
            //Crear archivo de excel
            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("Inventario_Clientes");

            //Obtener los datos de la tabla
            using (Inventario2024Entities iE = new Inventario2024Entities())
            {
                var consulta = (from cliente in iE.Clientes
                                select cliente).ToList();

                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("ID_Cliente");
                headerRow.CreateCell(1).SetCellValue("Primer Nombre");
                headerRow.CreateCell(2).SetCellValue("Apellido Paterno");
                headerRow.CreateCell(3).SetCellValue("Apellido Materno");
                headerRow.CreateCell(4).SetCellValue("Dirección");
                headerRow.CreateCell(5).SetCellValue("Telefono");

                //Escribir los datos en el archivo excel
                int rowIndex = 1;

                foreach (var dato in consulta)
                {
                    IRow row = sheet.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(dato.ID_Cliente);
                    row.CreateCell(1).SetCellValue(dato.p_nombre);
                    row.CreateCell(2).SetCellValue(dato.apPaterno);
                    row.CreateCell(3).SetCellValue(dato.apMaterno);
                    row.CreateCell(4).SetCellValue(dato.Direccion);
                    row.CreateCell(5).SetCellValue(dato.Telefono);

                    rowIndex++;
                }

                //Guardar el archivo de excel
                using (var fileStream = new FileStream("Inventario_Clientes.xlsx", FileMode.Create))
                {
                    wb.Write(fileStream);
                }

                MessageBox.Show("Se ha creado el informe de Clientes", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        private void frmPrincipal_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Desea salir?", "Información", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
