using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace Лаб4
{
    public class AdoAssistant
    {
        private string connectionString =
            ConfigurationManager.ConnectionStrings["connectionString_ADO"].ConnectionString;

        public DataTable TableLoad()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT ISBN, Автори, Назва, Видавництво, РікВидання
                    FROM Books";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                try
                {
                    con.Open();
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження даних: " + ex.Message);
                }
            }
            return dt;
        }

        public int InsertRecord(string isbn, string authors, string title, string publisher, int year)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Books
                        (ISBN, Автори, Назва, Видавництво, РікВидання)
                    VALUES
                        (@ISBN, @Автори, @Назва, @Видавництво, @РікВидання)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.Parameters.AddWithValue("@Автори", authors);
                    cmd.Parameters.AddWithValue("@Назва", title);
                    cmd.Parameters.AddWithValue("@Видавництво", publisher);
                    cmd.Parameters.AddWithValue("@РікВидання", year);
                    try
                    {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при вставці запису: " + ex.Message);
                    }
                }
            }
            return rowsAffected;
        }

        public int UpdateRecord(string isbn, string authors, string title, string publisher, int year)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE Books
                    SET Автори      = @Автори,
                        Назва       = @Назва,
                        Видавництво = @Видавництво,
                        РікВидання  = @РікВидання
                    WHERE ISBN = @ISBN";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.Parameters.AddWithValue("@Автори", authors);
                    cmd.Parameters.AddWithValue("@Назва", title);
                    cmd.Parameters.AddWithValue("@Видавництво", publisher);
                    cmd.Parameters.AddWithValue("@РікВидання", year);
                    try
                    {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при оновленні запису: " + ex.Message);
                    }
                }
            }
            return rowsAffected;
        }

        public int DeleteRecord(string isbn)
        {
            int rowsAffected = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Books WHERE ISBN = @ISBN";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    try
                    {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при видаленні запису: " + ex.Message);
                    }
                }
            }
            return rowsAffected;
        }
    }
}