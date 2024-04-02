using Inventario2024.Models;
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
    public partial class frmRegistroCliente : Form
    {
        public frmRegistroCliente()
        {
            InitializeComponent();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPnombre.Text = string.Empty;
            txtApPaterno.Text = string.Empty;
            txtApMaterno.Text = string.Empty;   
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            try { 
                string pNombre = txtPnombre.Text;
                string apPaterno = txtApPaterno.Text;
                string apMaterno = txtApMaterno.Text;
                string direccion = txtDireccion.Text;
                string telefono = txtTelefono.Text;

            
                using (Inventario2024Entities invE =  new Inventario2024Entities())
                {
                    //Condicional si el nombre completo del Cliente ya se encuentra registrado
                    string nombreCompleto = pNombre + " " + apPaterno + " " + apMaterno;

                    var consulta = from cliente in invE.Clientes
                                   select new
                                   {
                                       nombreCompletoLista = cliente.p_nombre + " " + cliente.apPaterno + " " + cliente.apMaterno,
                                   };

                    string nombre = consulta.ToString();

                    if (nombreCompleto.Equals(nombre))
                    {
                        //Si se encuentra registrado lo manda devuelta
                        MessageBox.Show($"El Cliente {nombre} ya se encuentra registrado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //Si no se encuentra registrado en el sistema, continua
                    Clientes nuevoCliente = new Clientes
                    {
                        ID_Cliente = GeneradorId.SiguienteId(),
                        p_nombre = pNombre,
                        apPaterno = apPaterno,
                        apMaterno = apMaterno,
                        Direccion = direccion,
                        Telefono = telefono,
                    };

                    //Agregamos el Cliente a la BD
                    invE.Clientes.Add(nuevoCliente);

                    //Guardamos los cambios en la BD
                    invE.SaveChanges();
                    MessageBox.Show("Se ha registrado un nuevo cliente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
            }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar Cliente: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public class GeneradorId
        {
            public static int SiguienteId()
            {
                Inventario2024Entities oInventarioE = new Inventario2024Entities();
                var ultimoId = (from cliente in oInventarioE.Clientes
                                orderby cliente.ID_Cliente descending
                                select cliente.ID_Cliente).FirstOrDefault();

                return ++ultimoId;
            }

        }

        private void btnRedGesCli_Click(object sender, EventArgs e)
        {
            frmGestionClientes frmGC = new frmGestionClientes();
            frmGC.Show();
        }
    }
}
