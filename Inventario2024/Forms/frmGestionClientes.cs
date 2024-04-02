using Inventario2024.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario2024.Forms
{
    public partial class frmGestionClientes : Form
    {
        //Variables Globales
        private int idCliente;

        public frmGestionClientes()
        {
            InitializeComponent();
        }

        //Carga de datos de la ventana
        private void frmGestionClientes_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        //Limpiar campos y tablas
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPnombre.Text = string.Empty;
            txtApPaterno.Text = string.Empty;
            txtApMaterno.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }

        //Carga de datos de BD
        private void cargarDatos()
        {
            Inventario2024Entities oInventarioE = new Inventario2024Entities();

            var listarClientes = (from cliente in oInventarioE.Clientes
                                  join ventas in oInventarioE.Ventas
                                  on cliente.ID_Cliente equals ventas.ID_Cliente
                                  into gestionCliente
                                  select new
                                  {
                                      ID_Cliente = cliente.ID_Cliente,
                                      Nombre_Completo = cliente.p_nombre + " " + cliente.apPaterno + " " + cliente.apMaterno,
                                      Ventas_por_Cliente = gestionCliente.Count()
                                  }).ToList();


            dgvClientes.DataSource = listarClientes;
        }

        //Boton refrescar tabla de datos de clientes
        private void btnRefrescarClientes_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        //Buscar cliente y mostrar datos del Cliente por ID
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Inicialización de BD
            Inventario2024Entities oInventarioE = new Inventario2024Entities();

            idCliente = int.Parse(txtBuscarCliente.Text);

            var listarCliente = from cliente in oInventarioE.Clientes
                                where cliente.ID_Cliente == idCliente
                                select cliente;

            // Verificción de ID
            if (listarCliente.Any())
            {
                // Cliente encontrado
                Clientes clientes = listarCliente.First();

                //Asignar Valores a los textbox
                txtPnombre.Text = clientes.p_nombre;
                txtApPaterno.Text = clientes.apPaterno;
                txtApMaterno.Text = clientes.apMaterno;
                txtDireccion.Text = clientes.Direccion;
                txtTelefono.Text = clientes.Telefono;

                //Asignar registro de ventas del cliente --

                var listaVentaCliente = from cliente in oInventarioE.Clientes
                                        join ventas in oInventarioE.Ventas
                                        on cliente.ID_Cliente equals ventas.ID_Cliente
                                        join producto in oInventarioE.Productos
                                        on ventas.ID_Producto equals producto.ID_Producto
                                        where cliente.ID_Cliente == idCliente
                                        select new
                                        {
                                            ventas.ID_Venta,
                                            producto.ID_Producto,
                                            producto.Nombre,
                                            producto.Precio,
                                            ventas.Cantidad,
                                        };

                dgvRegVenCli.DataSource = listaVentaCliente.ToList();

            }
            else
            {
                // Producto no encontrado
                MessageBox.Show("No se encontró el cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Editar datos del cliente
        private void btnEditarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                //Guardamos los datos dentro de las variables a utilizar
                string primerNombre = txtPnombre.Text;
                string apPaterno = txtApPaterno.Text;
                string apMaterno = txtApMaterno.Text;
                string direccion = txtDireccion.Text;
                string telefono = txtTelefono.Text;


                if (MessageBox.Show("Estas seguro de editar los datos del Cliente?", "Advertencia", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    //Inicializar BD 
                    Inventario2024Entities oInventarioE = new Inventario2024Entities();

                    //Identificar producto
                    var UpdCliente = (from cliente in oInventarioE.Clientes
                                       where cliente.ID_Cliente == idCliente
                                       select cliente).FirstOrDefault();

                    UpdCliente.p_nombre = primerNombre;
                    UpdCliente.apPaterno = apPaterno;
                    UpdCliente.apMaterno = apMaterno;
                    UpdCliente.Direccion = direccion;
                    UpdCliente.Telefono = telefono;


                    //Guardar cambios
                    oInventarioE.SaveChanges();

                    MessageBox.Show("Datos del cliente editados correctamente", "Información", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex}" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
