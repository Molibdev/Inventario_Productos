using Inventario2024.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Inventario2024.Forms
{
    public partial class frmRegistroVentas : Form
    {
        public frmRegistroVentas()
        {
            InitializeComponent();
        }

        private void btnRedGesInv_Click(object sender, EventArgs e)
        {
            frmGestionInventario frmGI = new frmGestionInventario();
            frmGI.Show();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtIdCliente.Text = string.Empty;
        }

        private void limpiarCampos()
        {
            txtIdProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtIdCliente.Text = string.Empty;
        }

        private void abrirNuevoUsuario()
        {
            frmRegistroCliente frmRC = new frmRegistroCliente();
            frmRC.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            try {
                //Tomar los datos de los textbox
                int idProducto = int.Parse(txtIdProducto.Text);
                int cantidadCompra = int.Parse(txtCantidad.Text);
                int idCliente = int.Parse(txtIdCliente.Text);

                //Inicializar BD
                Inventario2024Entities oInventarioE = new Inventario2024Entities();

                //Realizamos la consulta para la busqueda del usuario
                var existenciaCliente = from cliente in oInventarioE.Clientes
                                    where cliente.ID_Cliente == idCliente
                                    select cliente;

                var existenciaProducto = from producto in oInventarioE.Productos
                                         where producto.ID_Producto == idProducto &&
                                         producto.Cantidad_Stock > 0
                                         select producto;

                //Verificar existencia de producto
                if (existenciaProducto.Any())
                {
                    //Verificar existencia de Cliente
                    if (existenciaCliente.Any())
                    {
                        //Cliente existe, creamos la venta
                        using (oInventarioE)
                        {
                            //Creamos el objeto de Venta
                            Ventas nuevaVenta = new Ventas
                            {
                                ID_Venta = GeneradorId.SiguienteId(),
                                Fecha = DateTime.Today,
                                ID_Producto = idProducto,
                                Cantidad = cantidadCompra,
                                ID_Cliente = idCliente,
                            };

                            //Realizamos la actualizacion del stock
                            var UpdProducto = (from producto in oInventarioE.Productos
                                               where producto.ID_Producto == idProducto
                                               select producto).FirstOrDefault();


                            int calculoStock = UpdProducto.Cantidad_Stock.Value - cantidadCompra;

                            //Si el stock es menor a 0 no se realiza la venta por falta de existencias
                            if (calculoStock < 0)
                            {
                                MessageBox.Show("No hay existencias para realizar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            UpdProducto.Cantidad_Stock = calculoStock;

                            //Agregamos el producto a la BD
                            oInventarioE.Ventas.Add(nuevaVenta);

                            //Guardamos los cambios en la BD
                            oInventarioE.SaveChanges();
                            MessageBox.Show("Se ha registrado una nueva Venta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            //Limpiar campos de la venta al finalizar
                            limpiarCampos();
                        }
                    }
                    else
                    {
                        if(MessageBox.Show("No se ha encontrado el Cliente, desea registrarlo?", "Información", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            abrirNuevoUsuario();
                        }
                        return;
                    }
                } else
                {
                    MessageBox.Show("No se ha encontrado el producto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

 
            } catch (Exception ex)
            {
                MessageBox.Show($"Error al ingresar la venta: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public class GeneradorId
        {
            public static int SiguienteId()
            {
                Inventario2024Entities oInventarioE = new Inventario2024Entities();
                var ultimoId = (from ventas in oInventarioE.Ventas
                                orderby ventas.ID_Venta descending
                                select ventas.ID_Venta).FirstOrDefault();

                return ++ultimoId;
            }

        }
    }
}
