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
    public partial class frmGestionInventario : Form
    {
        //Variables globales
        private int idProducto;
        private byte[] bytesImagen;

        public frmGestionInventario()
        {
            InitializeComponent();
        }

        private void frmGestionInventario_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnRefrescarProductos_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnRedAgrProd_Click(object sender, EventArgs e)
        {
            frmRegistroProductos frmRP = new frmRegistroProductos();
            frmRP.Show();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            rtxtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            cbCategoria.Text = string.Empty;
            pbImagen.Image = null;
        }

        //Buscar Producto
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Inicialización de BD
            Inventario2024Entities oInventarioE = new Inventario2024Entities();

            idProducto = int.Parse(txtBuscar.Text);

            var listarProducto = from producto in oInventarioE.Productos
                                where producto.ID_Producto == idProducto
                                select producto;

            // Verificción de ID
            if (listarProducto.Any())
            {
                // Producto encontrado
                Productos productos = listarProducto.First();

                //Asignar Valores a los textbox
                txtNombre.Text = productos.Nombre;
                rtxtDescripcion.Text = productos.Descripcion;
                txtPrecio.Text = productos.Precio.ToString();
                txtStock.Text = productos.Cantidad_Stock.ToString();
                string id_categoria = productos.ID_Categoria.ToString();
                int id_categoria2 = int.Parse(id_categoria);
                
                switch (id_categoria2)
                {
                    case 1: cbCategoria.Text = "Electrónica"; break;
                    case 2: cbCategoria.Text = "Móviles"; break;
                    case 3: cbCategoria.Text = "Audio"; break;
                    case 4: cbCategoria.Text = "Electrodomésticos"; break;
                    case 5: cbCategoria.Text = "Oficina"; break;
                    default: cbCategoria.Text = string.Empty; break;
                }

                //Asignar valor al picture box
                try
                {
                    txtDireccionImagen.Text = string.Empty;
                    byte[] dataImagen = productos.Imagen.ToArray();
                    
                    using (MemoryStream ms = new MemoryStream(dataImagen))
                    {
                        Image img = Image.FromStream(ms);
                        pbImagen.Image = img;
                    }
                } catch
                {
                    pbImagen.Image = null;
                    MessageBox.Show("Error al cargar la imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                // Producto no encontrado
                MessageBox.Show("No se encontró el producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void cargarDatos()
        {
            // Inicializar BD
            Inventario2024Entities oInventarioE = new Inventario2024Entities();

            // Consulta con join
            var listaProductos = from producto in oInventarioE.Productos
                                 join categoria in oInventarioE.Categorias
                                 on producto.ID_Categoria equals categoria.ID_Categoria
                                 select new
                                 {
                                     producto.ID_Producto,
                                     producto.Nombre,
                                     producto.Precio,
                                     producto.Cantidad_Stock,
                                     CategoriaNombre = categoria.Nombre
                                 };

            dgvProductos.DataSource = listaProductos.ToList();
        }

        //Editar Producto
        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            try { 
                //Guardamos los datos dentro de las variables a utilizar
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

                               
                if(MessageBox.Show("Estas seguro de editar los datos del producto?", "Advertencia", MessageBoxButtons.OKCancel, 
                                   MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    //Inicializar BD 
                    Inventario2024Entities oInventarioE = new Inventario2024Entities();

                    //Identificar producto
                    var UpdProducto = (from producto in oInventarioE.Productos
                                       where producto.ID_Producto == idProducto
                                       select producto).FirstOrDefault();

                    UpdProducto.Nombre = nombre;
                    UpdProducto.Descripcion = descripcion;
                    UpdProducto.Precio = precio;
                    UpdProducto.Cantidad_Stock = stock;
                    UpdProducto.ID_Categoria = id_categoria;
                    UpdProducto.Imagen = imagen;

                    //Guardar cambios
                    oInventarioE.SaveChanges();

                    MessageBox.Show("Datos del producto editados correctamente", "Información", MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                } else
                {
                    return;
                }

            } catch
            {
                MessageBox.Show("Error al guardar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void btnSeleccionImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
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


    }
}
