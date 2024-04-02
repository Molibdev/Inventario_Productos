namespace Inventario2024.Forms
{
    partial class frmRegistroVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Venta = new System.Windows.Forms.GroupBox();
            this.btnRedGesInv = new System.Windows.Forms.Button();
            this.btnVenta = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.txtIdProducto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Venta.SuspendLayout();
            this.SuspendLayout();
            // 
            // Venta
            // 
            this.Venta.Controls.Add(this.btnVenta);
            this.Venta.Controls.Add(this.btnLimpiar);
            this.Venta.Controls.Add(this.label3);
            this.Venta.Controls.Add(this.label2);
            this.Venta.Controls.Add(this.txtIdCliente);
            this.Venta.Controls.Add(this.txtCantidad);
            this.Venta.Controls.Add(this.txtIdProducto);
            this.Venta.Controls.Add(this.label1);
            this.Venta.Location = new System.Drawing.Point(12, 39);
            this.Venta.Name = "Venta";
            this.Venta.Size = new System.Drawing.Size(366, 284);
            this.Venta.TabIndex = 0;
            this.Venta.TabStop = false;
            this.Venta.Text = "Venta de Productos";
            // 
            // btnRedGesInv
            // 
            this.btnRedGesInv.Location = new System.Drawing.Point(216, 10);
            this.btnRedGesInv.Name = "btnRedGesInv";
            this.btnRedGesInv.Size = new System.Drawing.Size(170, 23);
            this.btnRedGesInv.TabIndex = 8;
            this.btnRedGesInv.Text = "Gestión de Inventario";
            this.btnRedGesInv.UseVisualStyleBackColor = true;
            this.btnRedGesInv.Click += new System.EventHandler(this.btnRedGesInv_Click);
            // 
            // btnVenta
            // 
            this.btnVenta.Location = new System.Drawing.Point(9, 244);
            this.btnVenta.Name = "btnVenta";
            this.btnVenta.Size = new System.Drawing.Size(351, 27);
            this.btnVenta.TabIndex = 7;
            this.btnVenta.Text = "Añadir venta";
            this.btnVenta.UseVisualStyleBackColor = true;
            this.btnVenta.Click += new System.EventHandler(this.btnVenta_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(285, 10);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 6;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ID Cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cantidad";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Location = new System.Drawing.Point(9, 189);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(351, 20);
            this.txtIdCliente.TabIndex = 3;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(9, 127);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(351, 20);
            this.txtCantidad.TabIndex = 2;
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.Location = new System.Drawing.Point(9, 61);
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.Size = new System.Drawing.Size(351, 20);
            this.txtIdProducto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Producto";
            // 
            // frmRegistroVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 337);
            this.Controls.Add(this.btnRedGesInv);
            this.Controls.Add(this.Venta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmRegistroVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Ventas";
            this.Venta.ResumeLayout(false);
            this.Venta.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Venta;
        private System.Windows.Forms.Button btnVenta;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TextBox txtIdProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRedGesInv;
    }
}