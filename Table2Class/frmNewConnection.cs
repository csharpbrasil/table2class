using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Table2Class
{
    public partial class frmNewConnection : Form
    {
        private string _connectionString;
        private string _providerName;

        public string connectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public string providerName
        {
            get { return _providerName; }
            set { _providerName = value; }
                
        }

        public frmNewConnection()
        {
            InitializeComponent();
        }

        private void PreencheCampos()
        {
            if (_connectionString != null && _providerName != null && _connectionString.Length > 0 && _providerName.Length > 0)
            {
                string[] p = _connectionString.Split(';');

                cboProviders.SelectedIndex = cboProviders.Items.IndexOf(_providerName);

                txtServidor.Text = p[0].ToString().Split('=')[1];
                txtUsuario.Text = p[2].ToString().Split('=')[1];
                txtSenha.Text = p[3].ToString().Split('=')[1];
                txtBancoDados.Text = p[1].ToString().Split('=')[1];
            }
        }

        private Boolean TestaConexao()
        {
            Boolean ConexaoOK = false;

            try
            {
                string p = cboProviders.Items[cboProviders.SelectedIndex].ToString();
                string c = getConnectionString(p, txtServidor.Text, txtUsuario.Text, txtSenha.Text, txtBancoDados.Text);
                
                clData objData = new clData();
                objData.ConnectionString = c;
                objData.DbProvider = p;

                DataTable dt = objData.GetTables();

                if (dt.Rows.Count > 0)
                {
                    ConexaoOK = true;
                }
            }
            catch
            {
                ConexaoOK = false;
            }

            return ConexaoOK;
        }

        private string getConnectionString(string ProviderName, string Server, string User, string Password, string Database)
        {
            string ConnectionString = string.Empty;

            switch (ProviderName)
            {
                case "System.Data.SqlClient":
                    ConnectionString = "Data Source=" + Server + ";Initial Catalog=" + Database + ";User ID=" + User + ";Password=" + Password + ";Persist Security Info=True";
                    break;
                case "MySql.Data.MySqlClient":
                    ConnectionString = "server=" + Server + ";database=" + Database + ";uid=" + User + ";pwd=" + Password;
                    break;
            }

            return ConnectionString;
        }

        private void frmNewConnection_Load(object sender, EventArgs e)
        {
            PreencheCampos();
        }

        private void btnTestaConexao_Click(object sender, EventArgs e)
        {
            if (TestaConexao())
            {
                MessageBox.Show("Conexão realizada com sucesso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Falha ao realizar a conexão!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            providerName = cboProviders.Items[cboProviders.SelectedIndex].ToString();
            connectionString = getConnectionString(providerName, txtServidor.Text, txtUsuario.Text, txtSenha.Text, txtBancoDados.Text);
            this.Close();
        }
    }
}
