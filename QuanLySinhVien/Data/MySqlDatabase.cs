using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace QuanLySinhVien.Data
{
    public static class MySqlDatabase
    {
        private const string DefaultConnectionString = "Server=localhost;Port=3306;Database=QuanLySinhVien;Uid=root;Pwd=123456;CharSet=utf8mb4;";

        public static string GetConnectionString()
        {
            var settings = ConfigurationManager.ConnectionStrings["QuanLySinhVien"];
            return (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
                ? DefaultConnectionString
                : settings.ConnectionString;
        }

        public static MySqlConnection CreateConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }

        public static MySqlConnection OpenConnection()
        {
            var connection = CreateConnection();
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                connection.Dispose();
                throw new Exception("Không thể mở kết nối MySQL: " + ex.Message, ex);
            }
        }

        public static void CloseConnection(MySqlConnection connection)
        {
            if (connection == null) return;
            try
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            finally
            {
                connection.Dispose();
            }
        }

        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (var connection = OpenConnection())
            using (var command = new MySqlCommand(sql, connection))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteNonQuery();
            }
        }

        public static DataTable ExecuteDataTable(string sql, params MySqlParameter[] parameters)
        {
            using (var connection = OpenConnection())
            using (var command = new MySqlCommand(sql, connection))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }
                using (var adapter = new MySqlDataAdapter(command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }
    }
}
