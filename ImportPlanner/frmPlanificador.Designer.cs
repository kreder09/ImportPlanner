namespace ImportacionesPlanner
{
    partial class frmPlanificador
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlanificador));
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.nudCrecimiento = new System.Windows.Forms.NumericUpDown();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.nudLeadTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudFrecuencia = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBaseCalculo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblResumen = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblImportaciones = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.cmbFamilia = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrecimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeadTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrecuencia)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(405, 18);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(201, 21);
            this.cmbProveedor.TabIndex = 0;
            // 
            // nudCrecimiento
            // 
            this.nudCrecimiento.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudCrecimiento.Location = new System.Drawing.Point(522, 86);
            this.nudCrecimiento.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.nudCrecimiento.Name = "nudCrecimiento";
            this.nudCrecimiento.Size = new System.Drawing.Size(62, 20);
            this.nudCrecimiento.TabIndex = 2;
            this.nudCrecimiento.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(981, 10);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(126, 23);
            this.btnGenerar.TabIndex = 3;
            this.btnGenerar.Text = "Generar Reporte";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // dgvResultados
            // 
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(32, 153);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.Size = new System.Drawing.Size(1109, 527);
            this.dgvResultados.TabIndex = 4;
            // 
            // nudLeadTime
            // 
            this.nudLeadTime.Increment = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudLeadTime.Location = new System.Drawing.Point(745, 51);
            this.nudLeadTime.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudLeadTime.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudLeadTime.Name = "nudLeadTime";
            this.nudLeadTime.Size = new System.Drawing.Size(62, 20);
            this.nudLeadTime.TabIndex = 5;
            this.nudLeadTime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Proveedor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(676, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Lead-Time:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(813, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "dias";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(451, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Crecimiento:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(590, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "%";
            // 
            // nudFrecuencia
            // 
            this.nudFrecuencia.Increment = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudFrecuencia.Location = new System.Drawing.Point(745, 86);
            this.nudFrecuencia.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudFrecuencia.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudFrecuencia.Name = "nudFrecuencia";
            this.nudFrecuencia.Size = new System.Drawing.Size(62, 20);
            this.nudFrecuencia.TabIndex = 12;
            this.nudFrecuencia.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(673, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Frecuencia:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(813, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "dias";
            // 
            // cmbBaseCalculo
            // 
            this.cmbBaseCalculo.FormattingEnabled = true;
            this.cmbBaseCalculo.Location = new System.Drawing.Point(405, 53);
            this.cmbBaseCalculo.Name = "cmbBaseCalculo";
            this.cmbBaseCalculo.Size = new System.Drawing.Size(201, 21);
            this.cmbBaseCalculo.TabIndex = 15;
            this.cmbBaseCalculo.SelectedIndexChanged += new System.EventHandler(this.cmbBaseCalculo_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(365, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Base:";
            // 
            // lblResumen
            // 
            this.lblResumen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResumen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumen.Location = new System.Drawing.Point(32, 18);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(302, 98);
            this.lblResumen.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label10.Location = new System.Drawing.Point(856, 687);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(284, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Import Planner v1.1.1 - Desarrollado por Federico Frustacci";
            // 
            // lblImportaciones
            // 
            this.lblImportaciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImportaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportaciones.Location = new System.Drawing.Point(859, 51);
            this.lblImportaciones.Name = "lblImportaciones";
            this.lblImportaciones.Size = new System.Drawing.Size(282, 93);
            this.lblImportaciones.TabIndex = 19;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(32, 124);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(187, 20);
            this.txtBuscar.TabIndex = 20;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(225, 124);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 21;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // cmbFamilia
            // 
            this.cmbFamilia.FormattingEnabled = true;
            this.cmbFamilia.Location = new System.Drawing.Point(724, 18);
            this.cmbFamilia.Name = "cmbFamilia";
            this.cmbFamilia.Size = new System.Drawing.Size(201, 21);
            this.cmbFamilia.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(673, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Familia:";
            // 
            // frmPlanificador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 709);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFamilia);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblImportaciones);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbBaseCalculo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudFrecuencia);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudLeadTime);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.nudCrecimiento);
            this.Controls.Add(this.cmbProveedor);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPlanificador";
            this.Text = "Import Planner";
            this.Load += new System.EventHandler(this.frmPlanificador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCrecimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeadTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrecuencia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.NumericUpDown nudCrecimiento;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.NumericUpDown nudLeadTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudFrecuencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBaseCalculo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblImportaciones;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.ComboBox cmbFamilia;
        private System.Windows.Forms.Label label2;
    }
}

