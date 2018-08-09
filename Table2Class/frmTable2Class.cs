using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//-----------
using Extensibility;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
//-----------
using System.IO;

namespace Table2Class
{
    public partial class frmTable2Class : Form
    {
        private DTE2 _applicationObject;
        private string connectionString = string.Empty;
        private string providerName = string.Empty;
        private DataTable TabelasExistentes = new DataTable();
        private DataTable TabelasSelecioandas = new DataTable();

        public frmTable2Class(object applicationObject)
        {
            _applicationObject = (DTE2)applicationObject;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListaTabelas()
        {
            lstTabelasExistentes.Items.Clear();
            lstTabelasSelecionadas.Items.Clear();
            btnCreate.Enabled = false;

            txtConnectionString.Text = connectionString;
            clData objData = new clData();
            objData.ConnectionString = connectionString;
            objData.DbProvider = providerName;

            DataTable dtAux = objData.GetTables("COLUMNS");

            TabelasExistentes = new DataTable();
            TabelasExistentes.Columns.Add("NOME_TABELA", typeof(string));

            foreach (DataRow row in dtAux.Rows)
            {
                DataRow newRow = TabelasExistentes.NewRow();
                newRow["NOME_TABELA"] = row["TABLE_NAME"].ToString().ToUpper();
                TabelasExistentes.Rows.Add(newRow);
            }

            TabelasExistentes.AcceptChanges();

            DataView dv = new DataView(TabelasExistentes);
            DataTable dt = dv.ToTable(true, new string[] { "NOME_TABELA" });

            foreach (DataRow row in dt.Rows)
            {
                lstTabelasExistentes.Items.Add(row["NOME_TABELA"]);
            }

            TabelasExistentes = dt;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (lstTabelasSelecionadas.Items.Count > 0)
            {
                clData objData = new clData();
                objData.ConnectionString = connectionString;
                objData.DbProvider = providerName;
                
                DataTable dtAux = objData.GetTables("COLUMNS");

                TabelasSelecioandas = new DataTable();
                TabelasSelecioandas.Columns.Add("NOME_TABELA", typeof(string));
                TabelasSelecioandas.Columns.Add("NOME_COLUNA", typeof(string));
                TabelasSelecioandas.Columns.Add("TIPO_COLUNA", typeof(string));
                TabelasSelecioandas.Columns.Add("AUTONUMERACAO", typeof(bool));
                TabelasSelecioandas.TableName = txtNomeClasse.Text;

                foreach (DataRow row in dtAux.Rows)
                {
                    //string IS_NULLABLE = row["IS_NULLABLE"].ToString();
                    string DATA_TYPE = row["DATA_TYPE"].ToString();
                    //string NUMERIC_PRECISION = row["NUMERIC_PRECISION"].ToString();
                    //string NUMERIC_PRECISION_RADIX = row["NUMERIC_PRECISION_RADIX"].ToString();
                    //string NUMERIC_SCALE = row["NUMERIC_SCALE"].ToString();

                    //Boolean AUTONUMBER = (IS_NULLABLE.ToUpper() == "NO" && DATA_TYPE.ToUpper() == "INT" && NUMERIC_PRECISION == "10" && NUMERIC_PRECISION_RADIX == "10" && NUMERIC_SCALE == "0");

                    DataRow newRow = TabelasSelecioandas.NewRow();
                    newRow["NOME_TABELA"] = row["TABLE_NAME"].ToString().ToUpper();
                    newRow["NOME_COLUNA"] = row["COLUMN_NAME"].ToString();
                    newRow["TIPO_COLUNA"] = getDataType(DATA_TYPE);
                    //newRow["AUTONUMERACAO"] = AUTONUMBER;
                    TabelasSelecioandas.Rows.Add(newRow);
                }

                List<string> ListaTabelasSelecionadas = new List<string>();

                foreach (object Tabela in lstTabelasSelecionadas.Items)
                {
                    ListaTabelasSelecionadas.Add(Tabela.ToString());
                }

                TabelasSelecioandas.AcceptChanges();

                CreateClass(txtNomeClasse.Text, Functions.CreateCode(TabelasSelecioandas, ListaTabelasSelecionadas.ToArray(), providerName));
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecione as tabelas para gerar a classe");
            }
        }

        private string getDataType(string SqlDataType)
        {
            string DataType = string.Empty;

            switch (SqlDataType.ToUpper())
            {
                case "VARCHAR":
                    DataType = "string";
                    break;
                case "DECIMAL":
                    DataType = "decimal";
                    break;
                case "INT":
                    DataType = "int";
                    break;
                case "DATETIME":
                    DataType = "DateTime";
                    break;
                default:
                    DataType = "string";
                    break;
            }

            return DataType;
        }

        private void CreateClass(string NomeClasse, string Codigo)
        {
            try
            {
                Array proj = (_applicationObject.ActiveSolutionProjects as Array);

                if (proj.Length > 0)
                {
                    Project project = (proj.GetValue(0) as Project);
                    string ProjectPath = string.Concat(Path.GetDirectoryName(project.FullName), "\\");
                    string NewClass = NomeClasse;
                    string NewExt = ".cs";
                    string FullPathFile = ProjectPath + NewClass + NewExt;

                    if (!File.Exists(FullPathFile))
                    {
                        File.Create(FullPathFile);
                    }
                    else
                    {
                        string[] files = Directory.GetFiles(ProjectPath);

                        int i = 0;
                        Boolean exists = false;
                        foreach (string file in files)
                        {
                            if (file == (ProjectPath + NewClass + NewExt) && !exists)
                            {
                                i++;
                                exists = true;
                            }
                        }
                        if (i > 0)
                        {
                            foreach (string file in files)
                            {
                                if (exists && (file == ProjectPath + NewClass + "(" + i + ")" + NewExt))
                                {
                                    i++;
                                }
                            }

                            FullPathFile = ProjectPath + NewClass + "(" + i + ")" + NewExt;
                        }
                        else
                        {
                            FullPathFile = ProjectPath + NewClass + NewExt;
                        }

                        File.Create(FullPathFile);
                    }

                    project.ProjectItems.AddFromFile(FullPathFile);

                    _applicationObject.ItemOperations.OpenFile(FullPathFile, Constants.vsViewKindCode);
                    TextDocument objTextDoc = (TextDocument)_applicationObject.ActiveDocument.Object("TextDocument");
                    EditPoint objEditPoint = (EditPoint)objTextDoc.StartPoint.CreateEditPoint();
                    objEditPoint.Insert(Codigo);

                    project.SaveAs(project.FullName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Table2Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovaConexao_Click(object sender, EventArgs e)
        {
            frmNewConnection newConnection = new frmNewConnection();
            
            if (txtConnectionString.Text.Length > 0)
            {
                newConnection.connectionString = connectionString;
                newConnection.providerName = providerName;
            }

            newConnection.ShowDialog();

            connectionString = newConnection.connectionString;
            providerName = newConnection.providerName;

            if (connectionString != null && providerName != null && connectionString.Length > 0 && providerName.Length > 0)
            {
                gbxTabelas.Enabled = true;
                ListaTabelas();
            }
        }

        private void btnIncluirTodas_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTabelasExistentes.Items.Count; i++)
            {
                lstTabelasSelecionadas.Items.Add(lstTabelasExistentes.Items[i].ToString());
            }

            lstTabelasExistentes.Items.Clear();
            btnCreate.Enabled = true;
        }

        private void btnIncluirSelecionada_Click(object sender, EventArgs e)
        {
            if (lstTabelasExistentes.Items.Count > 0)
            {
                if (lstTabelasExistentes.SelectedItems.Count > 0)
                {
                    string NomeTabela = lstTabelasExistentes.SelectedItem.ToString();

                    lstTabelasExistentes.Items.Remove(NomeTabela);
                    lstTabelasSelecionadas.Items.Add(NomeTabela);
                }
                else
                {
                    MessageBox.Show("Selecione uma tabela.", "Table2Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (lstTabelasSelecionadas.Items.Count > 0)
            {
                btnCreate.Enabled = true;
            }
            else
            {
                btnCreate.Enabled = false;
            }
        }

        private void btnExcluirSelecionada_Click(object sender, EventArgs e)
        {
            if (lstTabelasSelecionadas.Items.Count > 0)
            {
                if (lstTabelasSelecionadas.SelectedItems.Count > 0)
                {
                    string NomeTabela = lstTabelasSelecionadas.SelectedItem.ToString();

                    lstTabelasSelecionadas.Items.Remove(NomeTabela);
                    lstTabelasExistentes.Items.Add(NomeTabela);
                }
                else
                {
                    MessageBox.Show("Selecione uma tabela.", "Table2Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (lstTabelasSelecionadas.Items.Count > 0)
            {
                btnCreate.Enabled = true;
            }
            else
            {
                btnCreate.Enabled = false;
            }
        }

        private void btnExcluirTodas_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTabelasSelecionadas.Items.Count; i++)
            {
                lstTabelasExistentes.Items.Add(lstTabelasSelecionadas.Items[i].ToString());
            }

            lstTabelasSelecionadas.Items.Clear();
            btnCreate.Enabled = false;
        }
    }
}
