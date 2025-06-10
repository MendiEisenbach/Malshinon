using System;
using MySql.Data.MySqlClient;

namespace Malshinon.Database
{
    public class DbConnection
    {
        static string connectionString = "Server=localhost;Database=malshinon_db;User=root;Password='';";

        public MySqlConnection connection;

        public void Connect()
        {
            var conn = new MySqlConnection(connectionString);
            connection = conn;
            try
            {
                conn.Open();
                Console.WriteLine("Connected to MySQL database successfully.");
                conn.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error connecting to MySQL database: {ex.Message}");
            }
        }

        public MySqlConnection GetOpenConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public void CloseConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }
        }


    }
}


