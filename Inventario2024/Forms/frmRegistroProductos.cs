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
    public partial class frmRegistroProductos : Form
    {
        //Variables globales
        private byte[] bytesImagen; 
        public frmRegistroProductos()
        {
            InitializeComponent();
        }

        private void btnRegistroProducto_Click(object sender, EventArgs e)
        {
            try { 
            //Guardamos los datos de los textbox
            int id = GeneradorId.SiguienteId();
            string nombre = txtNombre.Text;
            string descripcion = rtxtDescripcion.Text;
            decimal precio = decimal.Parse(txtPrecio.Text);
            int stock = int.Parse(txtStock.Text);
            string categoria = cbCategoria.Text;
            int id_categoria = 0;

            switch (categoria)
            {
                case "Electrónica": id_categoria = 1; break;
                case "Móviles": id_categoria = 2; break;
                case "Audio": id_categoria = 3; break;
                case "Electrodomésticos": id_categoria = 4; break;
                case "Oficina": id_categoria = 5; break;
                default: id_categoria = 0; break;
            }

            byte[] imagen = bytesImagen;

            //Llamamos a la base de datos
            using (Inventario2024Entities oInventarioE = new Inventario2024Entities())
            {
                //Creamos el objeto de producto
                Productos nuevoProducto = new Productos
                {
                    ID_Producto = id,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Cantidad_Stock = stock,
                    ID_Categoria = id_categoria,
                    Imagen = imagen,
                };

                //Agregamos el producto a la BD
                oInventarioE.Productos.Add(nuevoProducto);

                //Guardamos los cambios en la BD
                oInventarioE.SaveChanges();
                MessageBox.Show("Se ha registrado un nuevo producto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //Limpiar campos
                limpiarCampos();
                
            }
            } catch (Exception ex)
            {
                MessageBox.Show($"Error al ingresar el producto: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog  ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Cargar la imagen seleccionada en el PictureBox
                    pbImagen.Image = Image.FromFile(ofd.FileName);

                    // Guardar la imagen como byte[]
                    bytesImagen = File.ReadAllBytes(ofd.FileName);

                    // Mostrar la dirección de la imagen
                    txtDireccionImagen.Text = ofd.FileName;
                }
            }
        }

        public class GeneradorId
        {
            public static int SiguienteId()
            {
                Inventario2024Entities oInventarioE = new Inventario2024Entities();
                var ultimoId = (from producto in oInventarioE.Productos
                                orderby producto.ID_Producto descending
                                select producto.ID_Producto).FirstOrDefault();

                return ++ultimoId;
            }

        }

        private void limpiarCampos()
        {
            txtNombre.Text = string.Empty;
            rtxtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtDireccionImagen.Text= string.Empty;
            cbCategoria.Text = string.Empty;
            pbImagen.Image = null; 
        }

        private void btnRedGestProd_Click(object sender, EventArgs e)
        {
            frmGestionInventario frmGI = new frmGestionInventario();
            frmGI.Show();
        }


    }
}
