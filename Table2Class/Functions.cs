using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Table2Class
{
    public class Functions
    {
        public static string CreateCode(DataTable _DataTable, string[] ListaTabelasSelecionadas, string ProviderName)
        {
            #region Constroi a Classe
            StringBuilder classString = new StringBuilder();
            classString.AppendLine("/*                                  ");
            classString.AppendLine(" * GERADOR TABLE2CLASS              ");
            classString.AppendLine(" * AUTOR: RAPHAEL CARDOSO           ");
            classString.AppendLine(" * SITE: www.csharpbrasil.com.br    ");
            classString.AppendLine(" * VERSAO: 1.0                      ");
            classString.AppendLine(" */                                 ");

            classString.AppendLine("using System;                       ");
            classString.AppendLine("using System.Collections.Generic;   ");
            classString.AppendLine("using System.Text;                  ");
            classString.AppendLine("using System.Data;                  ");
            classString.AppendLine("using System.Data.Common;           ");
            classString.AppendLine("                                    ");
            classString.AppendLine("namespace " + _DataTable.TableName);
            classString.AppendLine("{");

            foreach (string Tabela in ListaTabelasSelecionadas)
            {
                classString.AppendLine("    public class " + Tabela.ToString() + " ");
                classString.AppendLine("    {");

                DataView dv = new DataView(_DataTable);
                dv.RowFilter = "NOME_TABELA = '" + Tabela  + "'";

                foreach (DataRow row in dv.ToTable().Rows)
                {
                    classString.AppendLine("        private " + row["TIPO_COLUNA"].ToString() + " _" + row["NOME_COLUNA"].ToString() + ";");
                    classString.AppendLine("        ");
                    classString.AppendLine("        public " + row["TIPO_COLUNA"].ToString() + " " + row["NOME_COLUNA"].ToString());
                    classString.AppendLine("        {");
                    classString.AppendLine("            get{ return _" + row["NOME_COLUNA"].ToString() + ";}");
                    classString.AppendLine("            set{ _" + row["NOME_COLUNA"].ToString() + " = value;}");
                    classString.AppendLine("        }");
                }

                classString.AppendLine("    }");
                classString.AppendLine("    ");
            }

            classString.AppendLine("}");

            #endregion

            return classString.ToString();
        }
    }
}
