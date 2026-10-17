using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;

namespace QuanLySinhVien.Data.DAL
{
    public class DataHelper
    {
        private DbConnection con;
        private string connectionString;
        private string providerName;

        public DataHelper()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["QuanLySinhVien"];
            if (settings == null)
            {
                settings = ConfigurationManager.ConnectionStrings["QLSV"];
            }

            // Fallback for test runners or PowerShell hosting where AppDomain config is not QuanLySinhVien.exe.config
            if (settings == null)
            {
                try
                {
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string exeConfigPath = Path.Combine(baseDir, "QuanLySinhVien.exe.config");
                    if (!File.Exists(exeConfigPath))
                    {
                        exeConfigPath = Path.Combine(baseDir, "..", "..", "App.config");
                    }

                    if (File.Exists(exeConfigPath))
                    {
                        var map = new ExeConfigurationFileMap { ExeConfigFilename = Path.GetFullPath(exeConfigPath) };
                        Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                        settings = config.ConnectionStrings.ConnectionStrings["QuanLySinhVien"]
                                ?? config.ConnectionStrings.ConnectionStrings["QLSV"];
                    }
                }
                catch { }
            }

            if (settings != null && !string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                this.connectionString = settings.ConnectionString;
                this.providerName = settings.ProviderName;
            }
            else
            {
                this.connectionString = @"Server=localhost;Port=3306;Database=QuanLySinhVien;Uid=root;Pwd=123456;CharSet=utf8mb4;";
                this.providerName = "MySql.Data.MySqlClient";
            }

            InitConnection();
        }

        public DataHelper(string connectionString, string providerName = null)
        {
            this.connectionString = connectionString;
            this.providerName = providerName;
            InitConnection();
        }

        private void InitConnection()
        {
            bool isMySql = (!string.IsNullOrEmpty(providerName) && providerName.IndexOf("MySql", StringComparison.OrdinalIgnoreCase) >= 0)
                        || (connectionString != null && (connectionString.IndexOf("Port=3306", StringComparison.OrdinalIgnoreCase) >= 0
                                                      || connectionString.IndexOf("Uid=", StringComparison.OrdinalIgnoreCase) >= 0));

            if (isMySql)
            {
                con = new MySqlConnection(connectionString);
            }
            else
            {
                con = new SqlConnection(connectionString);
            }
        }

        public void OpenConnection()
        {
            try
            {
                if (con == null) InitConnection();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể mở kết nối đến cơ sở dữ liệu: " + ex.Message, ex);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đóng kết nối cơ sở dữ liệu: " + ex.Message, ex);
            }
        }

        public DbParameter CreateParameter(string name, object value)
        {
            if (con is MySqlConnection)
            {
                return new MySqlParameter(name, value ?? DBNull.Value);
            }
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        public int ExecuteNonQuery(string sqlExcuteQuery, params DbParameter[] parameters)
        {
            try
            {
                OpenConnection();
                using (DbCommand comand = con.CreateCommand())
                {
                    comand.CommandText = sqlExcuteQuery;
                    if (parameters != null && parameters.Length > 0)
                    {
                        comand.Parameters.AddRange(parameters);
                    }
                    return comand.ExecuteNonQuery();
                }
            }
            catch (Exception exx)
            {
                throw new Exception(exx.Message, exx);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void ExecuteNonQuery(string sqlExcuteQuery)
        {
            ExecuteNonQuery(sqlExcuteQuery, null);
        }

        public DbDataReader ExcuteReader(string sqlSelect, params DbParameter[] parameters)
        {
            try
            {
                OpenConnection();
                DbCommand comand = con.CreateCommand();
                comand.CommandText = sqlSelect;
                if (parameters != null && parameters.Length > 0)
                {
                    comand.Parameters.AddRange(parameters);
                }
                return comand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exx)
            {
                CloseConnection();
                throw new Exception(exx.Message, exx);
            }
        }

        public DbDataReader ExcuteReader(string sqlSelect)
        {
            return ExcuteReader(sqlSelect, null);
        }

        public DbDataReader ExecuteReader(string sqlSelect, params DbParameter[] parameters)
        {
            return ExcuteReader(sqlSelect, parameters);
        }

        public DbDataReader ExecuteReader(string sqlSelect)
        {
            return ExcuteReader(sqlSelect, null);
        }

        public DataTable GetDataTable(string sqlSelect, params DbParameter[] parameters)
        {
            try
            {
                OpenConnection();
                using (DbCommand comand = con.CreateCommand())
                {
                    comand.CommandText = sqlSelect;
                    if (parameters != null && parameters.Length > 0)
                    {
                        comand.Parameters.AddRange(parameters);
                    }

                    DbDataAdapter da;
                    if (comand is MySqlCommand)
                        da = new MySqlDataAdapter((MySqlCommand)comand);
                    else
                        da = new SqlDataAdapter((SqlCommand)comand);

                    using (da)
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception exx)
            {
                throw new Exception(exx.Message, exx);
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable GetDataTable(string sqlSelect)
        {
            return GetDataTable(sqlSelect, null);
        }
    }
}
