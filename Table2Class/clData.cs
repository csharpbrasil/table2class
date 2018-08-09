using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.IO;

namespace Table2Class
{
	public class clData
    {
        #region Propriedades

        private string _DbProvider;
		private string _ConnectionString;
        private string _parameterChar = "@";
        private List<DbParameter> _parametros = new List<DbParameter>();
              
		private DbProviderFactory factory;
        private DbConnection Conn;
        private int _commandTimeout = 10000;
        private bool _usePrepare = false;
        private string _dateFormat = "dd/MM/yyyy HH:mm";

        /// <summary>
        /// Usado para definir os formatos de Data. dd- dias com duas casas, MM - Meses com duas casas, 
        /// yyyy - anos com 4 casas, HH - Horas no formato 24 horas com duas casas, mm - Minutos com duas casas,
        /// ss - Segundos com duas casas.
        /// Exemplo: para formatar 2008-12-01 em portugues use dd/MM/yyyy (01/12/2008)
        /// </summary>
        public string DateFormat
        {
            get { return _dateFormat; }
            set { _dateFormat = value; }
        }


        public bool UsePrepare
        {
            get { return _usePrepare; }
            set { _usePrepare = value; }
        }


        public string ParameterChar
        {
            get { return _parameterChar; }
            set { _parameterChar = value; }
        }

        public int CommandTimeout
        {
            get { return _commandTimeout; }
            set { _commandTimeout = value; }
        }

        /// <summary>
        /// Propriedade para passar os parametros utilizados no Comando ou DataSet
        /// </summary>
        public List<DbParameter> Parametros
        {
            get
            {                
                return _parametros;
            }
            set
            {
                foreach (DbParameter par in value)
                {
                    if (!par.ParameterName.StartsWith(_parameterChar))
                    {
                        par.ParameterName = _parameterChar + par.ParameterName;
                    }
                }
                _parametros = value;
            }
        }
		
        /// <summary>
        /// Seta o Provider
        /// </summary>
		public string DbProvider
		{
			get
			{
				return _DbProvider;	
			}
			set
			{
                switch (value)
                {
                    case "System.Data.SqlClient":
                        _parameterChar = "@";
                        break;
                    case "System.Data.OracleClient":
                        _parameterChar = ":";
                        break;
                    case "System.Data.Odbc":
                        _parameterChar = "?";
                        break;
                    case "System.Data.OleDb":
                        _parameterChar = "@";
                        break;
                    default:
                        _parameterChar = "@";
                        break;
                }
				_DbProvider = value;
				factory = DbProviderFactories.GetFactory(_DbProvider);
			}
		}

		public string ConnectionString
		{
			get
			{
				return _ConnectionString;
			}
			set
			{
				_ConnectionString = value;
			}
		}
        #endregion

        /// <summary>
        /// Abre conexão com o Banco de Dados
        /// </summary>
        /// <returns></returns>
		private System.Data.Common.DbConnection OpenDB()
		{
            try
            {
                Conn = factory.CreateConnection();
                Conn.ConnectionString = _ConnectionString;

                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();

                    if (_DbProvider == "System.Data.SqlClient")
                    {
                        DbCommand cmd = Conn.CreateCommand();
                        cmd.CommandText = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "SET DATEFORMAT DMY";
                        cmd.ExecuteNonQuery();
                    }
                    if (_DbProvider == "System.Data.OracleClient")
                    {
                        DbCommand cmd = Conn.CreateCommand();
                        cmd.CommandText = "alter session set NLS_DATE_FORMAT = \"DD/MM/YYYY HH24:MI\"";
                        cmd.ExecuteNonQuery();
                    }
                }

                return Conn;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao Conectar-se. Exception " + ex.Message);

            }
		}

        /// <summary>
        /// Fechado a Conexão com o Banco de Dados
        /// </summary>
        /// <param name="Conn">Informe a Conexão</param>
		private void CloseDB(System.Data.Common.DbConnection Conn)
		{
            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao Desconectar do Banco de Dados. Exception: " + ex.Message);
            }
		}

        private string TrataParametro(string strSQL, DbCommand cmd, DbParameter par)
        {
            cmd.Parameters.Add(par);
            string _auxPar = par.ParameterName.Replace(_parameterChar, ":");

            if (_auxPar.IndexOf(":") < 0)
            {
                _auxPar = ":" + _auxPar;
            }

            if (par.ParameterName.IndexOf(_parameterChar) >= 0)
            {
                strSQL = strSQL.ToUpper().Replace(_auxPar, par.ParameterName);
            }
            else
            {
                strSQL = strSQL.ToUpper().Replace(_auxPar, _parameterChar + par.ParameterName);
            }

            if (_DbProvider == "System.Data.Odbc")
            {
                strSQL = strSQL.ToUpper().Replace("?" + par.ParameterName, "?");
            }

            if (_parameterChar != ":")
                strSQL = strSQL.ToUpper().Replace(_auxPar, par.ParameterName);
            return strSQL;
        }

        /// <summary>
        /// Gera um Parametro já com seu valor
        /// </summary>
        /// <param name="tipo">Tipo de Dados</param>
        /// <param name="nome">Nome do Parametro</param>
        /// <param name="valor">Valor do Parametro</param>
        /// <returns></returns>
        public void IncluiParametro(DbType tipo, string nome, object valor)
        {
            DbParameter par = factory.CreateParameter();
            if (_DbProvider == "System.Data.Odbc")
            {
                par.DbType = tipo;
                par.ParameterName = nome.Replace("?", nome);
                par.Value = valor;
            }
            else
            {
                par.DbType = tipo;
                par.ParameterName = nome;
                par.Value = valor;
            }

            Parametros.Add(par);
        }

        /// <summary>
        /// Gera um Parametro já com seu valor
        /// </summary>
        /// <param name="tipo">Tipo de Dados</param>
        /// <param name="nome">Nome do Parametro</param>
        /// <param name="valor">Valor do Parametro</param>
        /// <returns></returns>
        public DbParameter GeraParametro(DbType tipo,string nome, object valor)
        {
            DbParameter par = factory.CreateParameter();
            if (_DbProvider == "System.Data.Odbc")
            {
                par.DbType = tipo;
                par.ParameterName = nome.Replace("?",nome);
                par.Value = valor;
            }
            else
            {
                par.DbType = tipo;
                par.ParameterName = nome;
                par.Value = valor;
            }

            return par;
        }

        /// <summary>
        /// Informando uma query SQL, é retornado o valor da primeira coluna da primeira linha 
        /// </summary>
        /// <param name="strSQL">Informe uma query SQL</param>
        /// <returns>Retorna o Objecto</returns>
        public object GetValor(string strSQL)
        {
            try
            {
                Conn = OpenDB();

                DbCommand cmd = Conn.CreateCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Parameters.Clear();
                if (_parametros != null)
                {
                    foreach (DbParameter par in _parametros)
                    {
                        strSQL = TrataParametro(strSQL, cmd, par);
                    }
                }

                cmd.CommandText = strSQL;

                object _intreg = cmd.ExecuteScalar();

                CloseDB(Conn);

                return _intreg;
            }
            catch (Exception ex)
            {
                _parametros.Clear();
                throw new Exception("Falha ao executar a instruçao: " + strSQL + " Exception: " + ex.Message);
            }
            finally
            {
                _parametros.Clear();
            }
        }

        /// <summary>
        /// Informando uma query SQL, é retornado o numero de linhas afetadas 
        /// </summary>
        /// <param name="strSQL">Informe uma query SQL</param>
        /// <returns>Retorna o numero de linhas afetadas</returns>
		public int ExeCmd(string strSQL)
		{
            try
            {
                Conn = OpenDB();
                DbCommand cmd = Conn.CreateCommand();
                cmd.CommandTimeout = _commandTimeout;
                if (_parametros != null)
                {
                    foreach (DbParameter par in _parametros)
                    {
                        strSQL = TrataParametro(strSQL, cmd, par);
                    }
                }

                cmd.CommandText = strSQL;
                int _intreg = cmd.ExecuteNonQuery();
                CloseDB(Conn);

                return _intreg;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao executar a instruçao: " + strSQL + " Exception: " + ex.Message);
            }
            finally
            {
                // Zera os parametros passados.
                _parametros.Clear();

            }

		}

        public DataTable GetTables()
        {
            Conn = OpenDB();
            
            DataTable tb = new DataTable();
            try
            {
              tb = Conn.GetSchema("Tables");
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi possivel retornar as tabelas do banco de dados. Erro: " + ex.Message);
            }
            finally
            {
                CloseDB(Conn);
            }

            return tb;
        }

        public DataTable GetTables(string collectionName)
        {
            Conn = OpenDB();

            DataTable tb = new DataTable();

            try
            {
                tb = Conn.GetSchema(collectionName);
            }
            catch (Exception ex)
            {
                throw new Exception("Nao foi possivel retornar as tabelas do banco de dados. Erro: " + ex.Message);
            }
            finally
            {
                CloseDB(Conn);
            }

            return tb;
        }

        /// <summary>
        /// Informando uma query SQL, é retornado um DataSet
        /// </summary>
        /// <param name="strSQL">Informe a query SQL</param>
        /// <returns>Retorna um DataSet</returns>
		public System.Data.DataSet GetDataSet(string strSQL)
		{
            try
            {
                Conn = OpenDB();
                DataSet ds = new DataSet();
                DbDataAdapter da = factory.CreateDataAdapter();
                DbCommand cmd = Conn.CreateCommand();

                cmd.CommandTimeout = _commandTimeout;
                cmd.Parameters.Clear();
                if (_parametros != null)
                {
                    foreach (DbParameter par in _parametros)
                    {
                        strSQL = TrataParametro(strSQL, cmd, par);
                    }
                }
                cmd.CommandText = strSQL;
                da.SelectCommand = cmd;

                if (_usePrepare)
                    cmd.Prepare();

                da.Fill(ds);
                CloseDB(Conn);


                return ds;
            }
            catch (Exception ex)
            {
                _parametros.Clear();
                throw new Exception("Falha ao retornar registros. Instrucao: " + strSQL + " Exception: " + ex.Message);
            }
            finally
            {
                _parametros.Clear();
            }
		}

        /// <summary>
        /// Informando uma query SQL e o nome da TABELA, é retornado um DataTable
        /// </summary>
        /// <param name="strSQL">Informe a query SQL</param>
        /// <param name="_tablename">Informe o nome da TABELA</param>
        /// <param name="top">Retorna uma quantidade especifica de registros</param>
        /// <returns>Retorna um DataTabela</returns>
        public System.Data.DataTable GetDataTable(string strSQL, string _tablename, int top)
        {
            try
            {
                Conn = OpenDB();
                DataTable dt = new DataTable();
   
                DbCommand cmd = Conn.CreateCommand();

                cmd.CommandTimeout = _commandTimeout;
                cmd.Parameters.Clear();
                if (_parametros != null)
                {

                    foreach (DbParameter par in _parametros)
                    {
                        strSQL = TrataParametro(strSQL, cmd, par);
                    }
                }
                cmd.CommandText = strSQL;

                if (_usePrepare)
                    cmd.Prepare();

                DbDataReader reader = cmd.ExecuteReader();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                }

                int pos = 0;

                while (reader.Read() && pos < top)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc.ColumnName] = reader[dc.ColumnName];
                    }
                    dt.Rows.Add(dr);
                    pos++;
                }

                if (reader.Read())
                {
                    cmd.Cancel();
                }
                //reader.Dispose();
                //reader.NextResult();
                //reader.Close();
                //da.SelectCommand = cmd;
                //da.Fill(dt);
                dt.TableName = _tablename;
                CloseDB(Conn);


                return dt;
            }
            catch (Exception ex)
            {
                _parametros.Clear();
                throw new Exception("Falha ao retornar registros. Instrucao: " + strSQL + " Exception: " + ex.Message);
            }
            finally
            {
                _parametros.Clear();
            }
        }

        /// <summary>
        /// Informando uma query SQL e o nome da TABELA, é retornado um DataTable
        /// </summary>
        /// <param name="strSQL">Informe a query SQL</param>
        /// <param name="_tablename">Informe o nome da TABELA</param>
        /// <returns>Retorna um DataTabela</returns>
		public System.Data.DataTable GetDataTable(string strSQL, string _tablename)
		{
            //strSQL = strSQL.Replace(":", _parameterChar);

            try
            {
                Conn = OpenDB();
                DataTable dt = new DataTable();
                DbDataAdapter da = factory.CreateDataAdapter();
                DbCommand cmd = Conn.CreateCommand();

                cmd.CommandTimeout = _commandTimeout;
                cmd.Parameters.Clear();
                if (_parametros != null)
                {

                    foreach (DbParameter par in _parametros)
                    {
                        strSQL = TrataParametro(strSQL, cmd, par);
                    }
                }
                cmd.CommandText = strSQL;
                da.SelectCommand = cmd;

                if (_usePrepare)
                    cmd.Prepare();

                da.Fill(dt);
                dt.TableName = _tablename;
                CloseDB(Conn);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao retornar registros. Instrucao: " + strSQL + " Exception: " + ex.Message);
            }
            finally
            {
                _parametros.Clear();
            }
		}

        /// <summary>
        /// Informando uma query SQl e o nome da TABELA, é retornado um DataView
        /// </summary>
        /// <param name="strSQL">Informe a query SQL</param>
        /// <param name="_tablename">Informe o nome da TABELA</param>
        /// <returns>Retorna um DataView</returns>
		public System.Data.DataView GetDataView(string strSQL, string _tablename)
		{
			DataView dv = new DataView(GetDataTable(strSQL, _tablename));
			return dv;
		}
    }
}
