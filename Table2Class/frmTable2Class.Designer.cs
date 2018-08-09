namespace Table2Class
{
    partial class frmTable2Class
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
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnNovaConexao = new System.Windows.Forms.Button();
            this.lstTabelasExistentes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstTabelasSelecionadas = new System.Windows.Forms.ListBox();
            this.btnIncluirTodas = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnIncluirSelecionada = new System.Windows.Forms.Button();
            this.btnExcluirSelecionada = new System.Windows.Forms.Button();
            this.btnExcluirTodas = new System.Windows.Forms.Button();
            this.gbxTabelas = new System.Windows.Forms.GroupBox();
            this.txtNomeClasse = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxTabelas.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(12, 9);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(91, 13);
            this.lblConnectionString.TabIndex = 0;
            this.lblConnectionString.Text = "ConnectionString:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.BackColor = System.Drawing.SystemColors.Window;
            this.txtConnectionString.Location = new System.Drawing.Point(109, 6);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.ReadOnly = true;
            this.txtConnectionString.Size = new System.Drawing.Size(217, 20);
            this.txtConnectionString.TabIndex = 1;
            // 
            // btnNovaConexao
            // 
            this.btnNovaConexao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovaConexao.Location = new System.Drawing.Point(332, 5);
            this.btnNovaConexao.Name = "btnNovaConexao";
            this.btnNovaConexao.Size = new System.Drawing.Size(24, 20);
            this.btnNovaConexao.TabIndex = 2;
            this.btnNovaConexao.Text = "...";
            this.btnNovaConexao.UseVisualStyleBackColor = true;
            this.btnNovaConexao.Click += new System.EventHandler(this.btnNovaConexao_Click);
            // 
            // lstTabelasExistentes
            // 
            this.lstTabelasExistentes.FormattingEnabled = true;
            this.lstTabelasExistentes.Location = new System.Drawing.Point(9, 32);
            this.lstTabelasExistentes.Name = "lstTabelasExistentes";
            this.lstTabelasExistentes.Size = new System.Drawing.Size(138, 173);
            this.lstTabelasExistentes.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Existentes";
            // 
            // lstTabelasSelecionadas
            // 
            this.lstTabelasSelecionadas.FormattingEnabled = true;
            this.lstTabelasSelecionadas.Location = new System.Drawing.Point(198, 32);
            this.lstTabelasSelecionadas.Name = "lstTabelasSelecionadas";
            this.lstTabelasSelecionadas.Size = new System.Drawing.Size(137, 173);
            this.lstTabelasSelecionadas.TabIndex = 5;
            // 
            // btnIncluirTodas
            // 
            this.btnIncluirTodas.Location = new System.Drawing.Point(153, 59);
            this.btnIncluirTodas.Name = "btnIncluirTodas";
            this.btnIncluirTodas.Size = new System.Drawing.Size(39, 20);
            this.btnIncluirTodas.TabIndex = 6;
            this.btnIncluirTodas.Text = ">>";
            this.btnIncluirTodas.UseVisualStyleBackColor = true;
            this.btnIncluirTodas.Click += new System.EventHandler(this.btnIncluirTodas_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Selecionadas";
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Enabled = false;
            this.btnCreate.Location = new System.Drawing.Point(200, 280);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(281, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnIncluirSelecionada
            // 
            this.btnIncluirSelecionada.Location = new System.Drawing.Point(153, 85);
            this.btnIncluirSelecionada.Name = "btnIncluirSelecionada";
            this.btnIncluirSelecionada.Size = new System.Drawing.Size(39, 20);
            this.btnIncluirSelecionada.TabIndex = 10;
            this.btnIncluirSelecionada.Text = ">";
            this.btnIncluirSelecionada.UseVisualStyleBackColor = true;
            this.btnIncluirSelecionada.Click += new System.EventHandler(this.btnIncluirSelecionada_Click);
            // 
            // btnExcluirSelecionada
            // 
            this.btnExcluirSelecionada.Location = new System.Drawing.Point(153, 111);
            this.btnExcluirSelecionada.Name = "btnExcluirSelecionada";
            this.btnExcluirSelecionada.Size = new System.Drawing.Size(39, 20);
            this.btnExcluirSelecionada.TabIndex = 11;
            this.btnExcluirSelecionada.Text = "<";
            this.btnExcluirSelecionada.UseVisualStyleBackColor = true;
            this.btnExcluirSelecionada.Click += new System.EventHandler(this.btnExcluirSelecionada_Click);
            // 
            // btnExcluirTodas
            // 
            this.btnExcluirTodas.Location = new System.Drawing.Point(153, 137);
            this.btnExcluirTodas.Name = "btnExcluirTodas";
            this.btnExcluirTodas.Size = new System.Drawing.Size(39, 20);
            this.btnExcluirTodas.TabIndex = 12;
            this.btnExcluirTodas.Text = "<<";
            this.btnExcluirTodas.UseVisualStyleBackColor = true;
            this.btnExcluirTodas.Click += new System.EventHandler(this.btnExcluirTodas_Click);
            // 
            // gbxTabelas
            // 
            this.gbxTabelas.Controls.Add(this.txtNomeClasse);
            this.gbxTabelas.Controls.Add(this.label3);
            this.gbxTabelas.Controls.Add(this.label1);
            this.gbxTabelas.Controls.Add(this.btnExcluirTodas);
            this.gbxTabelas.Controls.Add(this.lstTabelasExistentes);
            this.gbxTabelas.Controls.Add(this.btnExcluirSelecionada);
            this.gbxTabelas.Controls.Add(this.lstTabelasSelecionadas);
            this.gbxTabelas.Controls.Add(this.btnIncluirSelecionada);
            this.gbxTabelas.Controls.Add(this.btnIncluirTodas);
            this.gbxTabelas.Controls.Add(this.label2);
            this.gbxTabelas.Enabled = false;
            this.gbxTabelas.Location = new System.Drawing.Point(12, 32);
            this.gbxTabelas.Name = "gbxTabelas";
            this.gbxTabelas.Size = new System.Drawing.Size(344, 239);
            this.gbxTabelas.TabIndex = 13;
            this.gbxTabelas.TabStop = false;
            // 
            // txtNomeClasse
            // 
            this.txtNomeClasse.Location = new System.Drawing.Point(103, 211);
            this.txtNomeClasse.Name = "txtNomeClasse";
            this.txtNomeClasse.Size = new System.Drawing.Size(232, 20);
            this.txtNomeClasse.TabIndex = 14;
            this.txtNomeClasse.Text = "NewClass";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Nome da Classe:";
            // 
            // frmTable2Class
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(368, 312);
            this.Controls.Add(this.gbxTabelas);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnNovaConexao);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTable2Class";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Creating Class From Tables";
            this.gbxTabelas.ResumeLayout(false);
            this.gbxTabelas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnNovaConexao;
        private System.Windows.Forms.ListBox lstTabelasExistentes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstTabelasSelecionadas;
        private System.Windows.Forms.Button btnIncluirTodas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnIncluirSelecionada;
        private System.Windows.Forms.Button btnExcluirSelecionada;
        private System.Windows.Forms.Button btnExcluirTodas;
        private System.Windows.Forms.GroupBox gbxTabelas;
        private System.Windows.Forms.TextBox txtNomeClasse;
        private System.Windows.Forms.Label label3;
    }
}