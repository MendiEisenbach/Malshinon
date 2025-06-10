using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Malshinon.Models;
using Malshinon.Database;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Malshinon.DAL
{
    public class ReportDAL
    {
        private DbConnection dbConnection = new DbConnection();

        public void AddReport(IntelReport intelReportort)
        { 
            MySqlConnection conn = null;
            
            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = @"INSERT INTO IntelReports(Id, ReporterId, TargetId, Text)
                                VALUES(@Id, @ReporterId, @TargetId, @Text)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", intelReportort.Id);
                cmd.Parameters.AddWithValue("@ReporterId", intelReportort.ReporterId);
                cmd.Parameters.AddWithValue("@TargetId", intelReportort.TargetId);
                cmd.Parameters.AddWithValue("@Text", intelReportort.Text);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Reportort: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }
        }


        public IntelReport GetReportById(int id)
        {
            IntelReport? intelReport = null;
            MySqlConnection conn = null;

            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = "SELECT * FROM IntelReport WHERE id = @id LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    intelReport = new IntelReport
                    {
                        Id = reader.GetInt32("id"),
                        ReporterId = reader.GetInt32("ReporterId"),
                        TargetId = reader.GetInt32("TargetId"),
                        Text = reader.GetString("Text"),
                        Timestamp = reader.GetDateTime("Timestamp")
                    };
                }

            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"Error Get Reporter by Id: {ex.Message}");
            }

            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }
            return intelReport;
        
        }

        public List<IntelReport> GetReportsByReporter(int reporterId)
        {
            List<IntelReport> Reports = new List<IntelReport>();
            MySqlConnection conn = null;

            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = "SELECT * FROM IntelReport WHERE reporterId = @reporterId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@reporterId", reporterId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IntelReport IntelReport = new IntelReport
                    {
                        Id = reader.GetInt32("id"),
                        ReporterId = reader.GetInt32("ReporterId"),
                        TargetId = reader.GetInt32("TargetId"),
                        Text = reader.GetString("Text"),
                        Timestamp = reader.GetDateTime("Timestamp")
                    };

                    Reports.Add(IntelReport);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error Get Reports By Reporter: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }

            return Reports;
        }

        public List<IntelReport> GetReportsByTarget(int targetId)
        {
            List<IntelReport> Reports = new List<IntelReport>();
            MySqlConnection conn = null;

            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = "SELECT * FROM IntelReport WHERE targetId = @targetId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@reporterId", targetId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IntelReport IntelReport = new IntelReport
                    {
                        Id = reader.GetInt32("id"),
                        ReporterId = reader.GetInt32("ReporterId"),
                        TargetId = reader.GetInt32("TargetId"),
                        Text = reader.GetString("Text"),
                        Timestamp = reader.GetDateTime("Timestamp")
                    };

                    Reports.Add(IntelReport);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error Get Reports By Target: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }

            return Reports;
        }


        public IntelReport UpdateReport(IntelReport report)
        {
            MySqlConnection conn = null;
            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = @"UPDATE IntelReports 
                         SET ReporterId = @ReporterId, 
                             TargetId = @TargetId, 
                             Text = @Text 
                         WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ReporterId", report.ReporterId);
                cmd.Parameters.AddWithValue("@TargetId", report.ReporterId);
                cmd.Parameters.AddWithValue("@Text", report.Text);
                cmd.Parameters.AddWithValue("@id", report.Id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    Console.WriteLine("No report found with the specified ID.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error updating report: {ex.Message}");
            }
            finally
            {
                if (conn != null)
                {
                    dbConnection.CloseConnection(conn);
                }
            }
            return report;
        }






















        //public void UpdateReport(IntelReport report) { /* Update Report Logic */ }
    }
}
