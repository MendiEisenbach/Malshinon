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

        public void AddReport(IntelReport intelReport)
        { 
            MySqlConnection conn = null;
            
            try
            {
                conn = dbConnection.GetOpenConnection();

                string query = @"INSERT INTO IntelReports (reporter_id, target_id, TEXT)
                         VALUES (@reporter_id, @target_id, @text)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@reporter_id", intelReport.ReporterId);
                cmd.Parameters.AddWithValue("@target_id", intelReport.TargetId);
                cmd.Parameters.AddWithValue("@text", intelReport.Text);

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

                string query = "SELECT * FROM IntelReports WHERE id = @id LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    intelReport = new IntelReport
                    {
                        Id = reader.GetInt32("id"),
                        ReporterId = reader.GetInt32("reporter_id"),
                        TargetId = reader.GetInt32("target_id"),
                        Text = reader.GetString("TEXT"),
                        Timestamp = reader.GetDateTime("TIMESTAMP")
                    };
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error Get Report by Id: {ex.Message}");
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

                string query = "SELECT * FROM IntelReports WHERE reporter_id = @reporterId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@reporterId", reporterId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   
                    IntelReport IntelReport = new IntelReport
                    {
                            Id = reader.GetInt32("id"),
                            ReporterId = reader.GetInt32("reporter_id"),
                            TargetId = reader.GetInt32("target_id"),
                            Text = reader["TEXT"].ToString(),
                            Timestamp = Convert.ToDateTime(reader["TIMESTAMP"])
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

                string query = "SELECT * FROM IntelReports WHERE target_id = @targetId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@targetId", targetId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IntelReport IntelReport = new IntelReport
                    {
                        Id = reader.GetInt32("id"),
                        ReporterId = reader.GetInt32("reporter_id"),
                        TargetId = reader.GetInt32("target_id"),
                        Text = reader.GetString("TEXT"),
                        Timestamp = reader.GetDateTime("TIMESTAMP")
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
                 SET reporter_id = @ReporterId, 
                     target_id = @TargetId, 
                     TEXT = @Text 
                 WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ReporterId", report.ReporterId);
                cmd.Parameters.AddWithValue("@TargetId", report.TargetId);
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

    }
}

