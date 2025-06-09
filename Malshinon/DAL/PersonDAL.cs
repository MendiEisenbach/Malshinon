using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Malshinon.Models;
using Malshinon.Database;


namespace Malshinon.DAL
{
    public class PersonDAL
    {
        private DbConnection dbConnection = new DbConnection();
        

        public void AddPerson(Person person)
        {
            MySqlConnection conn = null;
            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = @"INSERT INTO People (first_name, last_name, secret_code, type, num_reports, num_mentions) 
                         VALUES (@first_name, @last_name, @secret_code, @type, @num_reports, @num_mentions)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@first_name", person.FirstName);
                cmd.Parameters.AddWithValue("@last_name", person.LastName);
                cmd.Parameters.AddWithValue("@secret_code", person.SecretCode);
                cmd.Parameters.AddWithValue("@type", person.Type);
                cmd.Parameters.AddWithValue("@num_reports", person.NumReports);
                cmd.Parameters.AddWithValue("@num_mentions", person.NumMentions);

                cmd.ExecuteNonQuery();
            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"Error adding person: {ex.Message}");
            }

            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }
        }

        public Person GetPersonByName(string firstName, string lastName)
        {
            Person? person = null;
            MySqlConnection conn = null;

            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = "SELECT * FROM People WHERE first_name = @first_name AND last_name = @last_name LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@first_name", firstName);
                cmd.Parameters.AddWithValue("@last_name", lastName);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    person = new Person
                    {
                        Id = reader.GetInt32("id"),
                        FirstName = reader.GetString("first_name"),
                        LastName = reader.GetString("last_name"),
                        SecretCode = reader.GetString("secret_code"),
                        Type = reader.GetString("type"),
                        NumReports = reader.GetInt32("num_reports"),
                        NumMentions = reader.GetInt32("num_mentions")
                    };
                }

            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"Error Get Person By Name: {ex.Message}");
            }

            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }
            return person;
        }

        public void UpdatePerson(Person person)
        {
            MySqlConnection conn = null;
            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = @"UPDATE People 
                         SET first_name = @first_name, 
                             last_name = @last_name, 
                             secret_code = @secret_code, 
                             type = @type, 
                             num_reports = @num_reports, 
                             num_mentions = @num_mentions 
                         WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@first_name", person.FirstName);
                cmd.Parameters.AddWithValue("@last_name", person.LastName);
                cmd.Parameters.AddWithValue("@secret_code", person.SecretCode);
                cmd.Parameters.AddWithValue("@type", person.Type);
                cmd.Parameters.AddWithValue("@num_reports", person.NumReports);
                cmd.Parameters.AddWithValue("@num_mentions", person.NumMentions);
                cmd.Parameters.AddWithValue("@id", person.Id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    Console.WriteLine("No person found with the specified ID.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error updating person: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }
        }

        public void DeletePersonById(int id)
        {
            MySqlConnection conn = null;
            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = "DELETE FROM People WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    Console.WriteLine("No person found with the specified ID to delete.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error deleting person: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }
        }

    }

}


