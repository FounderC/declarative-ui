using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace Лаб4
{
    public class AdoAssistant
    {
        private string connectionString = ConfigurationManager
            .ConnectionStrings["connectionString_ADO"].ConnectionString;

        private DataTable dt = null;

        public DataTable TableLoad()
        {
            if (dt != null)
                return dt;

            dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ISBN, Автори, Видавництво, РікВидання FROM Books";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                try
                {
                    con.Open();

                    if (con.State == ConnectionState.Open)
                    {
                        MessageBox.Show("Підключення до бази даних успішне!");
                    }

                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка підключення до БД: " + ex.Message);
                }
            }

            return dt;
        }
    }
}